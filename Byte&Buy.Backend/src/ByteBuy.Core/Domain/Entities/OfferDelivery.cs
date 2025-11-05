using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public class OfferDelivery : AuditableEntity, ISoftDelete
{
    public Guid OfferId { get; set; }
    public Offer Offer { get; set; } = null!;
    public Guid DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
