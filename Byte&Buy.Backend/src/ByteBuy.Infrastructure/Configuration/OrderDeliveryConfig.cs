using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderDeliveryConfig : IEntityTypeConfiguration<OrderDelivery>
{
    public void Configure(EntityTypeBuilder<OrderDelivery> builder)
    {
        // Basic Fields
        builder.Property(od => od.DeliveryName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(od => od.CarrierCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(od => od.Channel)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(od => od.Order)
               .WithOne()
               .HasForeignKey<OrderDelivery>(od => od.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        // Owner type price
        builder.OwnsOne(od => od.Price, price =>
        {
            price.Property(p => p.Currency)
                 .HasMaxLength(3)
                 .IsRequired();

            price.Property(p => p.Amount)
                 .HasPrecision(18, 3)
                 .IsRequired();
        });


        builder.Property(od => od.City)
            .HasMaxLength(50);

        builder.Property(od => od.Street)
            .HasMaxLength(50);

        builder.Property(od => od.HouseNumber)
            .HasMaxLength(20);

        builder.Property(od => od.PostalCity)
            .HasMaxLength(50);

        builder.Property(od => od.PostalCode)
            .HasMaxLength(20);

        builder.Property(od => od.FlatNumber)
            .HasMaxLength(10);

        builder.Property(od => od.BuyerFullName);
        builder.Property(od => od.Phone);

        builder.Property(od => od.PickupPointId);

        builder.Property(od => od.PickupPointName)
            .HasMaxLength(50);

        builder.Property(od => od.PickupStreet)
            .HasMaxLength(50);

        builder.Property(od => od.PickupCity)
            .HasMaxLength(50);

        builder.Property(od => od.PickupLocalNumber)
            .HasMaxLength(20);

        builder.Property(od => od.ParcelLockerId);

        builder.HasIndex(od => od.OrderId);
        builder.HasIndex(od => od.Channel);

        // Globalny filter
        builder.HasQueryFilter(od => od.IsActive);
    }
}
