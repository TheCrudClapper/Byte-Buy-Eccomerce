using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(50);
        builder.OwnsOne(d => d.Price, d =>
        {
            d.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
            d.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();

            builder.HasQueryFilter(item => item.IsActive);
        });
    }
}
