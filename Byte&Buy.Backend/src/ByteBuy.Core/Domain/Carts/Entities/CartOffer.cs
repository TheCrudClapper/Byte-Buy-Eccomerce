using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Offers.Base;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Carts.Entities;

public class CartOffer : Entity, ISoftDeletable
{
    public int Quantity { get; protected set; }
    public Guid CartId { get; protected set; }
    public Guid OfferId { get; protected set; }
    public Cart Cart { get; protected set; } = null!;
    public Offer Offer { get; protected set; } = null!;
    public bool IsActive { get; protected set; }
    public DateTime? DateDeleted { get; protected set; }

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
            return Result.Failure(CartErrors.QuantityInvalid);

        return Result.Success();
    }

    public Result SetQuantity(int quantity)
    {
        var validate = Validate(quantity);
        if (validate.IsFailure)
            return validate;

        Quantity = quantity;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    //used only in Add scenarios of cart offer to load 
    //navigation prop used in price recalculation
    internal void AssignOffer(Offer offer)
    {
        Offer = offer;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
