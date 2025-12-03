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

        //return Result.Success(new PortalUser(firstName, lastName, email));
        return new PortalUser(firstName, lastName, email);
    }

    public static Result<PortalUser> CreateWithAddress(
        string firstName,
        string lastName,
        string email,
        Address address,
        IEnumerable<Guid>? revokedPermissions,
        IEnumerable<Guid>? grantedPermissions
       )
    {
        var portalUserResult = Create(firstName, lastName, email);
        if (portalUserResult.IsFailure)
            return Result.Failure<PortalUser>(portalUserResult.Error);

        var user = portalUserResult.Value;

        if(address is null)
            return Result.Failure<PortalUser>(Error.Validation("Address can't be null!"));

        user.AssignAddress(address);
        user.AssignPermissionsToUser(revokedPermissions ?? [], grantedPermissions ?? []);
        return user;
    }

    public void AssignCart(Cart cart)
    {
        Cart = cart;
    }

    public void AssignAddress(Address address)
    {
        address.AssignToUser(this);
        Addresses.Add(address);
    }
}
