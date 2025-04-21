using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShoppingList.Application.Commands;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Persistence;
using MyShoppingList.Persistence.Repositories;

namespace MyShoppingList.Configurator;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureDepencencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyShoppingListDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        services.AddTransient<AddItemInGroupHandler>();
        services.AddTransient<CompleteItemHandler>();
        services.AddTransient<CreateGroupHandler>();
        services.AddTransient<CreateItemHandler>();
        services.AddTransient<GetAllGroupsHandler>();
        services.AddTransient<GetGroupByIdHandler>();
        services.AddTransient<RemoveItemFromGroupHandler>();
        services.AddTransient<UncompleteItemHandler>();

        return services;
    }

    public static void ApplyMigrations(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MyShoppingListDbContext>();
        dbContext.Database.Migrate();
    }
}
