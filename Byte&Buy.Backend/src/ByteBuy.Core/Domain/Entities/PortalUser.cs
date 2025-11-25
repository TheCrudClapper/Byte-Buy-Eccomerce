using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class PortalUser : ApplicationUser
{
    public ICollection<Address> Addresses { get; private set; } = new List<Address>();
    public ICollection<Order> Orders { get; private set; } = new List<Order>();
    public Cart Cart { get; private set; } = null!;

    private PortalUser(string firstName, string lastName, string email) 
        : base(firstName, lastName, email, null) { }

    public static Result<PortalUser> Create(string firstName, string lastName, string email)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, null);
        if (validationResult.IsFailure)
            return Result.Failure<PortalUser>(validationResult.Error);

        return Result.Success(new PortalUser(firstName, lastName, email));
    }

    public void AssignCart(Cart cart)
    {
        Cart = cart;
    }
}
