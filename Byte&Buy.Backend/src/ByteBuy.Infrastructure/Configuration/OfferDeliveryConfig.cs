using ByteBuy.Core.Domain.Offers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OfferDeliveryConfig : IEntityTypeConfiguration<OfferDelivery>
{
    public void Configure(EntityTypeBuilder<OfferDelivery> builder)
    {
        builder.HasOne(od => od.Offer)
            .WithMany(o => o.OfferDeliveries)
            .HasForeignKey(od => od.OfferId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(od => od.Delivery)
            .WithMany(o => o.OfferDeliveries)
            .HasForeignKey(od => od.DeliveryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(od => od.IsActive);
    }
}
