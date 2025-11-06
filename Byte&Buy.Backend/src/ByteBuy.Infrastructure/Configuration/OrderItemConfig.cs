using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oi => oi.Offer)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OfferId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(oi => oi.UnitPrice, oi =>
        {
            oi.Property(prop => prop.Amount).HasColumnName("OrderItem_UnitPrice").HasPrecision(18, 3);
            oi.Property(prop => prop.Currency).HasColumnName("OrderItem_UnitPriceCurrency").HasMaxLength(3);
        });

        builder.OwnsOne(oi => oi.TotalPrice, oi =>
        {
            oi.Property(prop => prop.Amount).HasColumnName("OrderItem_TotalPrice").HasPrecision(18, 3);
            oi.Property(prop => prop.Currency).HasColumnName("OrderItem_TotalPriceCurrency").HasMaxLength(3);
        });

    }
}
