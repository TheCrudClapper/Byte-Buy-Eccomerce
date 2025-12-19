using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

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

    private Offer() { }
    
    protected Offer(Guid itemId, Guid createdByUserId, int quantityAvailable)
    {
        ItemId = itemId;
        CreatedByUserId = createdByUserId;
        QuantityAvailable = quantityAvailable;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }

    public void DeactivateForPortalUser()
    {
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        Item.Deactivate();
    }

    public static Result ValidateBasicInfo(int quantityAvailable)
    {
        if (quantityAvailable < 1)
            return Result.Failure(Error.Validation("Quantity must be higher than 0"));
        return Result.Success();
    }

    public void AssignDeliveriesToOffer(IEnumerable<Guid> deliveryIds)
    {
        foreach(var deliveryId in deliveryIds)
        {
            OfferDeliveries.Add(OfferDelivery.Create(Id, deliveryId));
        }
    }
}
