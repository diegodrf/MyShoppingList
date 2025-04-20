using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyShoppingList.Persistence;

namespace MyShoppingList.Configurator;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureDepencencies(this IServiceCollection services)
    {
        services.AddDbContext<MyShoppingListDbContext>(options =>
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=myshoppinglistdevdb;User Id=postgres;Password=fk#Ws#ho5!SZMvHHW;"));
        return services;
    }
}
