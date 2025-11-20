using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class PortalUser : ApplicationUser
{
    public ICollection<Address> Addresses { get; private set; } = new List<Address>();
    public ICollection<Order> Orders { get; private set; } = new List<Order>();
    public Cart Cart { get; private set; } = null!;

    private PortalUser(string firstName, string lastName, string email) 
        : base(firstName, lastName, email) { }

    public static Result<PortalUser> Create(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<PortalUser>(Error.Validation("First name cannot be empty"));
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<PortalUser>(Error.Validation("Last name cannot be empty"));
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<PortalUser>(Error.Validation("Email cannot be empty"));

        return Result.Success(new PortalUser(firstName, lastName, email));
    }

    public void AssignCart(Cart cart)
    {
        Cart = cart;
    }
}
