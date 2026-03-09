using ByteBuy.Core.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ItemConfig : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {

        builder.Property(i => i.Name).IsRequired().HasMaxLength(75);
        builder.Property(i => i.Description).IsRequired().HasMaxLength(2000);

        builder.HasOne(i => i.Condition)
            .WithMany(i => i.Products)
            .HasForeignKey(p => p.ConditionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(i => i.CategoryId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(i => i.Images)
            .WithOne(img => img.Item)
            .HasForeignKey(img => img.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(i => i.Offers)
            .WithOne(o => o.Item)
            .HasForeignKey(i => i.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
