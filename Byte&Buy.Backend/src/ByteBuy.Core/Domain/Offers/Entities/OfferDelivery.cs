using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Deliveries;

namespace ByteBuy.Core.Domain.Offers.Entities;

public class OfferDelivery : Entity, ISoftDeletable
{
    public Guid OfferId { get; private set; }
    public Offer Offer { get; private set; } = null!;
    public Guid DeliveryId { get; private set; }
    public Delivery Delivery { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private OfferDelivery() { }

    public OfferDelivery(Guid offerId, Guid deliveryId)
    {
        OfferId = offerId;
        DeliveryId = deliveryId;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public void Reactivate()
    {
        IsActive = true;
        DateDeleted = null;
    }

    public static OfferDelivery Create(Guid offerId, Guid deliveryId)
        => new OfferDelivery(offerId, deliveryId);
}
