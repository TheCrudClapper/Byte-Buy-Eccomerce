using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class DeliveryCarrierConfig : IEntityTypeConfiguration<DeliveryCarrier>
{
    public void Configure(EntityTypeBuilder<DeliveryCarrier> builder)
    {
        builder.Property(dc => dc.Code).HasMaxLength(20).IsRequired();
        builder.Property(dc => dc.Name).HasMaxLength(50).IsRequired();

        builder.HasMany(dc => dc.Deliveries)
            .WithOne(dc => dc.DeliveryCarrier)
            .HasForeignKey(dc => dc.DeliveryCarrierId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(dc => dc.IsActive);
    }
}
