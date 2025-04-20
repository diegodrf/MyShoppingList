using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Persistence.Configurations;
public class ItemGroupConfiguration : IEntityTypeConfiguration<ItemGroup>
{
    public void Configure(EntityTypeBuilder<ItemGroup> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.ItemId);
        builder.HasIndex(x => x.GroupId);
        builder.HasIndex(x => x.Completed_At);
        builder.HasIndex(x => new { x.ItemId, x.GroupId });

        builder.HasOne(x => x.Item)
            .WithMany(x => x.Groups)
            .HasForeignKey(x => x.ItemId);

        builder.HasOne(x => x.Group)
            .WithMany(g => g.Items)
            .HasForeignKey(ig => ig.GroupId);
    }
}