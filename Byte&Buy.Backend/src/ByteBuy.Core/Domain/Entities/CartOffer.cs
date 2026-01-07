using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class CartOffer : AuditableEntity, ISoftDeletable
{
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Guid OfferId { get; set; }
    public Cart Cart { get; set; } = null!;
    public Offer Offer { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    protected CartOffer() { }

    protected CartOffer(Guid cartId, Guid offerId, int quantity)
    {
        CartId = cartId;
        OfferId = offerId;
        Quantity = quantity;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
    }

    public static Result Validate(int quantity)
    {
        if (quantity <= 0)
            return Result.Failure(Error.Validation("Quantity must be greater than 0."));

        return Result.Success();
    }
        
    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
