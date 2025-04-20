using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Persistence.Configurations;
public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(g => g.RemovedAt);
    }
}
