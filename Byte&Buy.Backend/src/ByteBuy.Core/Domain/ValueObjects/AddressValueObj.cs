using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects;

public class AddressValueObj
{
    public string Street { get; private set; } = null!;
    public string HouseNumber { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public string? FlatNumber { get; private set; }

    private AddressValueObj() { }

    private AddressValueObj(
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string country,
        string? flatNumber = null)
    {
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        City = city;
        Country = country;
        FlatNumber = flatNumber;
    }

    public static Result Validate(string street, string houseNumber, string postalCode,
        string city, string country, string? flatNumber, IAddressValidationService validator)
    {
        var validatorResult = validator.Validate(street, houseNumber, postalCode, city, flatNumber);
        if (validatorResult.IsFailure)
            return validatorResult;

        if (string.IsNullOrWhiteSpace(country) || country.Length > 50)
            return Result.Failure(Error.Validation("Country is required and must be at most 50 characters."));

        return Result.Success();
    }

    public static Result<AddressValueObj> Create(
        string street, string houseNumber,string postalCode, string city, string country, string? flatNumber, IAddressValidationService validator)
    {
        var validationResult = Validate(street, houseNumber, postalCode, city, country, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<AddressValueObj>(validationResult.Error);

        return new AddressValueObj(street, houseNumber, postalCode, city, country, flatNumber);
    }
}

