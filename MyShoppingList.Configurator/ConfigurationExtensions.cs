using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyShoppingList.Application.Commands;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Persistence;
using MyShoppingList.Persistence.Repositories;

namespace MyShoppingList.Configurator;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureDepencencies(this IServiceCollection services)
    {
        services.AddDbContext<MyShoppingListDbContext>(options =>
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myshoppinglistdevdb;User Id=postgres;Password=fk#Ws#ho5!SZMvHHW;"));

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
}
