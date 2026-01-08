using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class ShippingAddress : AuditableEntity, ISoftDeletable
{
    public string Label { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string HouseNumber { get; private set; } = null!;
    public string PostalCity { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string? FlatNumber { get; private set; }
    public Guid UserId { get; private set; }
    public PortalUser User { get; private set; } = null!;
    public Guid CountryId { get; private set; }
    public Country Country { get; private set; } = null!;
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private ShippingAddress() { }
    private ShippingAddress(string label, string city,
        string street, string houseNumber,
        string postalCity, string postalCode,
        string? flatNumber, Guid countryId, bool isDefault)
    {
        Label = label;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCity = postalCity;
        PostalCode = postalCode;
        FlatNumber = flatNumber;
        CountryId = countryId;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
        IsDefault = isDefault;
    }

    public static Result Validate(string label, string city, string street, string houseNumber,
        string postalCity, string postalCode, string? flatNumber, IAddressValidationService validator)
    {
        var validatorResult = validator.Validate(street, houseNumber, postalCity, postalCode, city, flatNumber);
        if (validatorResult.IsFailure)
            return validatorResult;

        if (string.IsNullOrWhiteSpace(label) || label.Length > 50)
            return Result.Failure(PortalUserErrors.ShippingAddressLabelInvalid);

        return Result.Success();
    }

    public static Result<ShippingAddress> Create(string label, string city, string street,
        string houseNumber, string postalCity, string postalCode, string? flatNumber, Guid countryId, bool isDefault,
        IAddressValidationService validator)
    {
        var validationResult = Validate(label, city, street, houseNumber, postalCity, postalCode, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<ShippingAddress>(validationResult.Error);

        return new ShippingAddress(label, city, street, houseNumber, postalCity, postalCode, flatNumber, countryId, isDefault);
    }

    public Result Update(string label, string city, string street,
        string houseNumber, string postalCity, string postalCode, string? flatNumber, Guid countryId, bool isDefault,
        IAddressValidationService validator)
    {
        var validationResult = Validate(label, city, street, houseNumber, postalCity, postalCode, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<ShippingAddress>(validationResult.Error);

        Label = label;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCity = postalCity;
        PostalCode = postalCode;
        FlatNumber = flatNumber;
        IsDefault = isDefault;
        CountryId = countryId;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

    public void MarkAsDefault()
    {
        if (IsDefault)
            return;

        IsDefault = true;
    }

    public void UnmarkAsDefault()
    {
        if (!IsDefault)
            return;

        IsDefault = false;
    }

    public void AssignToUser(PortalUser user)
    {
        UserId = user.Id;
        User = user;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}