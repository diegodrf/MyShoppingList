using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingList.Domain.Entities;
using System.Reflection;

namespace MyShoppingList.Persistence;

/// <summary>
/// Migrations: dotnet ef migrations add Initial -c MyShoppingListDbContext -s MyShoppingList.WebApi -p MyShoppingList.Persistence
/// Update: dotnet ef database update -c MyShoppingListDbContext -s MyShoppingList.WebApi -p MyShoppingList.Persistence
/// </summary>
public class MyShoppingListDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<ItemGroup> ItemGroups { get; set; }
    public DbSet<Item> Items { get; set; }

    public MyShoppingListDbContext(DbContextOptions<MyShoppingListDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(MyShoppingListDbContext))!);
        SeedWithExample(modelBuilder);
    }

    public static void SeedWithExample(ModelBuilder modelBuilder)
    {
        var createdAt = new DateTime(2025, 4, 20, 2, 21, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Item>().HasData(new Item
        {
            Id = 1,
            Name = "Sample item",
            CreatedAt = createdAt
        });

        modelBuilder.Entity<Group>().HasData(new Group
        {
            Id = 1,
            Name = "Sample group",
            CreatedAt = createdAt
        });

        modelBuilder.Entity<ItemGroup>().HasData(new ItemGroup
        {
            Id = 1,
            ItemId = 1,
            GroupId = 1,
        });
    }
}
