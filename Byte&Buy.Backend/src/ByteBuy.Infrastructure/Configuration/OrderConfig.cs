using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        //enum -> string
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsOne(o => o.TotalAmount, o =>
        {
            o.Property(prop => prop.Amount).HasPrecision(18, 3);
            o.Property(prop => prop.Currency).HasMaxLength(3);
        });

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(o => o.OrderDate)
           .IsRequired();

        builder.OwnsOne(o => o.ShippingAddress, o =>
        {
            o.Property(p => p.City).HasMaxLength(50);
            o.Property(p => p.Street).HasMaxLength(50);
            o.Property(p => p.PostalCode).HasMaxLength(20);
            o.Property(p => p.HouseNumber).HasMaxLength(10);
            o.Property(p => p.Country).HasMaxLength(50);
            o.Property(p => p.FlatNumber).HasMaxLength(10);
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
