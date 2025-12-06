using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public class Address : AuditableEntity, ISoftDeletable
{
    public string Label { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string HouseNumber { get; private set; } = null!;
    public string PostalCity { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string? FlatNumber { get; private set; }
    public Guid? UserId { get; private set; }
    public PortalUser? User { get; private set; }
    public Guid CountryId { get; private set; }
    public Country Country { get; private set; } = null!;
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? DateDeleted { get; private set; }

    private Address() { }
    private Address(string label, string city,
        string street, string houseNumber,
        string postalCity, string postalCode,
        string? flatNumber, Country country, bool isDefault)
    {
        Label = label;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCity = postalCity;
        PostalCode = postalCode;
        FlatNumber = flatNumber;
        Country = country;
        CountryId = country.Id;
        IsActive = true;
        DateCreated = DateTime.UtcNow;
        IsDefault = isDefault;
    }

    public static Result Validate(string label, string city, string street, string houseNumber,
        string postalCity, string postalCode, string? flatNumber, IAddressValidationService validator)
    {
        var validatorResult = validator.Validate(street, houseNumber, postalCode, city, flatNumber);
        if (validatorResult.IsFailure)
            return validatorResult;

        if (string.IsNullOrWhiteSpace(label) || label.Length > 50)
            return Result.Failure(Error.Validation("Label is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(postalCity) || postalCity.Length > 50)
            return Result.Failure(Error.Validation("PostalCity is required and must be at most 50 characters."));

        return Result.Success();
    }

    public static Result<Address> Create(string label, string city, string street,
        string houseNumber, string postalCity, string postalCode, string? flatNumber, Country country, bool isDefault,
        IAddressValidationService validator)
    {
        var validationResult = Validate(label, city, street, houseNumber, postalCity, postalCode, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<Address>(validationResult.Error);

        if (country is null)
            return Result.Failure<Address>(Error.Validation("Country can't be null"));

        return new Address(label, city, street, houseNumber, postalCity, postalCode, flatNumber, country, isDefault);
    }

    public Result Update(string label, string city, string street,
        string houseNumber, string postalCity, string postalCode, string? flatNumber, Country country, bool isDefault,
        IAddressValidationService validator)
    {
        var validationResult = Validate(label, city, street, houseNumber, postalCity, postalCode, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<Address>(validationResult.Error);

        Label = label;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        PostalCity = postalCity;
        PostalCode = postalCode;
        FlatNumber = flatNumber;
        IsDefault = isDefault;
        Country = country;
        CountryId = country.Id;
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