using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.ValueObjects;

public class AddressValueObject : ValueObject
{
    public string Street { get; private set; } = null!;
    public string HouseNumber { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string PostalCity { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public string? FlatNumber { get; private set; }

    private AddressValueObject() { }

    private AddressValueObject(
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber = null)
    {
        Street = street;
        HouseNumber = houseNumber;
        PostalCode = postalCode;
        PostalCity = postalCity;
        City = city;
        Country = country;
        FlatNumber = flatNumber;
    }

    public static Result Validate(string street, string houseNumber, string postalCity, string postalCode,
        string city, string country, string? flatNumber, IAddressValidationService validator)
    {
        var validatorResult = validator.Validate(street, houseNumber, postalCity, postalCode, city, flatNumber);
        if (validatorResult.IsFailure)
            return validatorResult;

        if (string.IsNullOrWhiteSpace(country) || country.Length > 50)
            return Result.Failure(AddressErrors.CountryInvalid);

        return Result.Success();
    }

    public static Result<AddressValueObject> Create(
        string street, string houseNumber, string postalCity, string postalCode, string city, string country, string? flatNumber, IAddressValidationService validator)
    {
        var validationResult = Validate(street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);
        if (validationResult.IsFailure)
            return Result.Failure<AddressValueObject>(validationResult.Error);

        return new AddressValueObject(street, houseNumber, postalCity, postalCode, city, country, flatNumber);
    }

    public AddressValueObject Copy()
    {
        return new AddressValueObject(Street, HouseNumber, PostalCity, PostalCode, City, Country, FlatNumber);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return HouseNumber;
        yield return PostalCity;
        yield return PostalCode;
        yield return City;
        yield return Country;
        yield return FlatNumber;
    }
}

