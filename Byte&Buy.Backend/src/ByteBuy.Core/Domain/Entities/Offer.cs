using ByteBuy.Core.Domain.EntityContracts;

namespace ByteBuy.Core.Domain.Entities;

public abstract class Offer : AuditableEntity, ISoftDeletable
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;
    public ICollection<OfferDelivery> OfferDeliveries { get; set; } = new List<OfferDelivery>();
    public ICollection<CartOffer> CartOffers { get; set; } = new List<CartOffer>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public int QuantityAvailable { get; set; }
    public Guid CreatedByUserId { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
