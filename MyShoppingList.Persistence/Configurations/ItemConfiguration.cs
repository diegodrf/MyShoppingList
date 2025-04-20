using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Persistence.Configurations;
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.RemovedAt);
    }
}
