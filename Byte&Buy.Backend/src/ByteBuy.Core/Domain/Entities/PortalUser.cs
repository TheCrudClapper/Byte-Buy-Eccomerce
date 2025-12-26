using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class PortalUser : ApplicationUser
{
    public ICollection<Address> Addresses { get; private set; } = new List<Address>();
    public ICollection<Order> Orders { get; private set; } = new List<Order>();
    public Cart Cart { get; private set; } = null!;

    private PortalUser(string firstName, string lastName, string email, string? phoneNumber)
        : base(firstName, lastName, email, phoneNumber) { }


    public static Result<PortalUser> Create(string firstName, string lastName, string email, string? phoneNumber)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validationResult.IsFailure)
            return Result.Failure<PortalUser>(validationResult.Error);

        return new PortalUser(firstName, lastName, email, phoneNumber);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (var address in Addresses)
            address.Deactivate();
    }

    public static Result<PortalUser> CreateWithAddress(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        Address address,
        IEnumerable<Guid>? revokedPermissions,
        IEnumerable<Guid>? grantedPermissions)
    {
        var portalUserResult = Create(firstName, lastName, email, phoneNumber);
        if (portalUserResult.IsFailure)
            return Result.Failure<PortalUser>(portalUserResult.Error);

        var user = portalUserResult.Value;

        if (address is null)
            return Result.Failure<PortalUser>(Error.Validation("Address can't be null!"));

        user.AssignAddress(address);
        user.AssignPermissionsToUser(revokedPermissions ?? [], grantedPermissions ?? []);
        return user;
    }

    public Result Update(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateEdited = DateTime.UtcNow;
        PhoneNumber = phoneNumber;

        return Result.Success();
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

    public Result<Address> AddAddress(
    string label,
    string city,
    string street,
    string houseNumber,
    string postalCity,
    string postalCode,
    string? flatNumber,
    Guid countryId,
    bool isDefault,
    IAddressValidationService validator)
    {
        var addressResult = Address.Create(
            label,
            city,
            street,
            houseNumber,
            postalCity,
            postalCode,
            flatNumber,
            countryId,
            isDefault,
            validator);

        if (addressResult.IsFailure)
            return Result.Failure<Address>(addressResult.Error);

        var address = addressResult.Value;

        if (Addresses.Any(a => a.Label == address.Label))
            return Result.Failure<Address>(AddressErrors.DuplicateLabel);

        if (isDefault)
        {
            var currentDefault = Addresses.FirstOrDefault(a => a.IsDefault);
            currentDefault?.UnmarkAsDefault();
            address.MarkAsDefault();
        }
        address.AssignToUser(this);
        Addresses.Add(address);

        return address;
    }

    public Result UpdateAddress(
        Guid addressId, string label, string city, string street, string houseNumber, string postalCity,
        string postalCode, string? flatNumber, Guid countryId, bool isDefault, IAddressValidationService validator)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);
        if (address is null)
            return Result.Failure(Error.NotFound);

        if (Addresses.Any(a => a.Id != addressId && a.Label == label))
            return Result.Failure(AddressErrors.DuplicateLabel);

        if (address.IsDefault && !isDefault)
            return Result.Failure(AddressErrors.CannotUnsetCurrentDefault);

        if (!address.IsDefault && isDefault)
        {
            var currentDefault = Addresses.FirstOrDefault(a => a.IsDefault);
            currentDefault?.UnmarkAsDefault();
        }

        var result = address.Update(
           label, city, street, houseNumber,
           postalCity, postalCode, flatNumber,
           countryId, isDefault, validator);

        return result;
    }

    public Result RemoveAddress(Guid addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);
        if (address is null)
            return Result.Failure(Error.NotFound);

        if (address.IsDefault)
            return Result.Failure(AddressErrors.CannotDeleteCurrentDefault);

        address.Deactivate();
        return Result.Success();
    }
}
