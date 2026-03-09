using ByteBuy.Core.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class SaleOrderLineConfig : IEntityTypeConfiguration<SaleOrderLine>
{
    public void Configure(EntityTypeBuilder<SaleOrderLine> builder)
    {
        builder.OwnsOne(so => so.PricePerItem, so =>
        {
            so.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
            so.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
        });
    }
}
