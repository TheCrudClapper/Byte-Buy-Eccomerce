using ByteBuy.Core.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class RentOrderLineConfig : IEntityTypeConfiguration<RentOrderLine>
{
    public void Configure(EntityTypeBuilder<RentOrderLine> builder)
    {
        builder.OwnsOne(ro => ro.PricePerDay, ro =>
        {
            ro.Property(prop => prop.Currency).HasMaxLength(3);
            ro.Property(prop => prop.Amount).HasPrecision(18, 3);
        });
    }
}
