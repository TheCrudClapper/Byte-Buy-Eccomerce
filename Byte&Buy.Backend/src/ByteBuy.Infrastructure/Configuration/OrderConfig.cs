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
            .HasConversion<string>();

        builder.OwnsOne(o => o.TotalAmount, o =>
        {
            o.Property(prop => prop.Amount).HasPrecision(18, 3);
            o.Property(prop => prop.Currency).HasMaxLength(3);
        });
    }
}
