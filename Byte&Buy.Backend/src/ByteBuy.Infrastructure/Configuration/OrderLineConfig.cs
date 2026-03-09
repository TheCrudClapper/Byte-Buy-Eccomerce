using ByteBuy.Core.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderLineConfig : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.OwnsOne(ol => ol.Thumbnail, i =>
        {
            i.Property(img => img.AltText).HasMaxLength(50);
            i.Property(img => img.ImagePath);
        });

        builder.HasOne(ol => ol.Order)
            .WithMany(o => o.Lines)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(ol => ol.IsActive);
    }
}
