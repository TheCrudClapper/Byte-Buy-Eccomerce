using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.OwnsOne(d => d.Price, d =>
        {
            d.Property(prop => prop.Currency).HasMaxLength(3);
            d.Property(prop => prop.Amount).HasPrecision(18, 3);
        });
    }
}
