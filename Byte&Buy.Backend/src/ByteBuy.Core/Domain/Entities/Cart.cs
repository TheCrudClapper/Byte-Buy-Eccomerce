using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Cart : AuditableEntity, ISoftDeletable
{
    public Guid UserId { get; private set; }
    //EF Navigation Property ONLY
    public PortalUser User { get; private set; } = null!;
    public Money TotalCartValue { get; private set; } = Money.Zero;
    public Money TotalItemsValue { get; private set; } = Money.Zero;
    public ICollection<CartOffer> CartOffers { get; private set; } = new List<CartOffer>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
    private Cart() { }
    private Cart(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TotalCartValue = Money.Zero;
        TotalItemsValue = Money.Zero;
        DateCreated = DateTime.UtcNow;
        IsActive = true;
    }

    public static Result<Cart> Create(Guid userId)
    {
        if (userId == Guid.Empty)
            return Result.Failure<Cart>(Error.Validation("UserId cannot be empty"));

        return Result.Success(new Cart(userId));
    }
}


