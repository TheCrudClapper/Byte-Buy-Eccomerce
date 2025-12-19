using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class OfferDelivery : AuditableEntity, ISoftDeletable
{
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; } = null!;
    public Guid DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }


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

    public static OfferDelivery Create(Guid offerId, Guid deliveryId)
        => new OfferDelivery(offerId, deliveryId);
}
