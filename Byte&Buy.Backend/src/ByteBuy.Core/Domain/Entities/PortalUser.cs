using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class PortalUser : ApplicationUser
{
    public ICollection<ShippingAddress> ShippingAddresses { get; private set; } = [];
    public Guid CartId { get; private set; }


    //EF Navigation Properties ONLY
    public Cart Cart { get; private set; } = null!;
    public ICollection<Order> Orders { get; private set; } = [];
    public ICollection<Rental> Rentals { get; private set; } = [];

    private PortalUser(string firstName, string lastName, string email, string? phoneNumber)
        : base(firstName, lastName, email, phoneNumber) { }


    public static Result<PortalUser> Create(string firstName, string lastName, string email, string? phoneNumber)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validationResult.IsFailure)
            return Result.Failure<PortalUser>(validationResult.Error);

        return new PortalUser(firstName, lastName, email, phoneNumber);
    }

    public Result UpdateBasicInfo(string firstName, string lastName, string email, string? phoneNumber)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validationResult.IsFailure)
            return Result.Failure<PortalUser>(validationResult.Error);

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        UserName = email;

        return Result.Success(validationResult);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (var address in ShippingAddresses)
            address.Deactivate();
    }

    public static Result<PortalUser> CreateWithAddress(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        IEnumerable<Guid>? revokedPermissions,
        IEnumerable<Guid>? grantedPermissions,
        IAddressValidationService validator)
    {
        var portalUserResult = Create(firstName, lastName, email, phoneNumber);
        if (portalUserResult.IsFailure)
            return Result.Failure<PortalUser>(portalUserResult.Error);

        var user = portalUserResult.Value;

        var addressResult = user.SetHomeAddress(
            street,
            houseNumber,
            postalCity,
            postalCode,
            city,
            country,
            flatNumber,
            validator);

        if (addressResult.IsFailure)
            return Result.Failure<PortalUser>(addressResult.Error);

        user.AssignPermissionsToUser(revokedPermissions ?? [], grantedPermissions ?? []);
        return user;
    }

    public Result Update(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        IEnumerable<Guid>? grantedPermissionIds,
        IEnumerable<Guid>? revokedPermissionIds)
    {
        var validationResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Error);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = email;
        DateEdited = DateTime.UtcNow;
        PhoneNumber = phoneNumber;

        var permissionOverrideResult = SetPermissionOverrides(revokedPermissionIds, grantedPermissionIds);
        if (permissionOverrideResult.IsFailure)
            return Result.Failure(permissionOverrideResult.Error);

        return Result.Success();
    }

    // Used for creating new value object for home address
    public Result SetHomeAddress(
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        IAddressValidationService validator)
    {
        var addressResult = AddressValueObject.Create(
           street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public void AttachCart(Guid cartId)
    {
        if (cartId == Guid.Empty)
            throw new ArgumentException("CartId cannot be empty", nameof(cartId));

        CartId = cartId;
    }

    public Result<ShippingAddress> AddShippingAddress(
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
        var addressResult = ShippingAddress.Create(
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
            return Result.Failure<ShippingAddress>(addressResult.Error);

        var address = addressResult.Value;

        if (ShippingAddresses.Any(a => a.Label == address.Label))
            return Result.Failure<ShippingAddress>(PortalUserErrors.DuplicateShippingAddressLabel);

        //when there is not default address set, force the first one to be default
        var currentDefault = ShippingAddresses.FirstOrDefault(a => a.IsDefault);
        if (currentDefault is null)
        {
            address.MarkAsDefault();
        }
        else if (isDefault)
        {
            currentDefault.UnmarkAsDefault();
            address.MarkAsDefault();
        }

        address.AssignToUser(this);
        ShippingAddresses.Add(address);

        return address;
    }

    public Result UpdateShippingAddress(
        Guid addressId, string label, string city, string street, string houseNumber, string postalCity,
        string postalCode, string? flatNumber, Guid countryId, bool isDefault, IAddressValidationService validator)
    {
        var address = ShippingAddresses.FirstOrDefault(a => a.Id == addressId);
        if (address is null)
            return Result.Failure(PortalUserErrors.ShippingAddressNotFound);

        if (ShippingAddresses.Any(a => a.Id != addressId && a.Label == label))
            return Result.Failure(PortalUserErrors.DuplicateShippingAddressLabel);

        if (address.IsDefault && !isDefault)
            return Result.Failure(PortalUserErrors.CannotUnsetCurrentDefault);

        if (!address.IsDefault && isDefault)
        {
            var currentDefault = ShippingAddresses.FirstOrDefault(a => a.IsDefault);
            currentDefault?.UnmarkAsDefault();
        }

        var result = address.Update(
           label, city, street, houseNumber,
           postalCity, postalCode, flatNumber,
           countryId, isDefault, validator);

        return result;
    }

    public Result RemoveShippingAddress(Guid addressId)
    {
        var address = ShippingAddresses.FirstOrDefault(a => a.Id == addressId);
        if (address is null)
            return Result.Failure(Error.NotFound);

        if (address.IsDefault)
            return Result.Failure(PortalUserErrors.CannotDeleteCurrentDefault);

        address.Deactivate();
        return Result.Success();
    }
}
