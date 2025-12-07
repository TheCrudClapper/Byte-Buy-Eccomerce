using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Cart : AuditableEntity, ISoftDeletable
{
    public Guid UserId { get; private set; }
    public PortalUser User { get; private set; } = null!;
    public Money TotalCartValue { get; private set; } = Money.Zero;
    public Money TotalItemsValue { get; private set; } = Money.Zero;
    public ICollection<CartOffer> CartOffers { get; private set; } = new List<CartOffer>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
    private Cart() { }
    private Cart(PortalUser user)
    {
        User = user;
        UserId = user.Id;
        TotalCartValue = Money.Zero;
        TotalItemsValue = Money.Zero;
        DateCreated = DateTime.UtcNow;
        IsActive = true;
    }

    public static Result<Cart> Create(PortalUser user)
    {
        return Result.Success(new Cart(user));
    }
}


