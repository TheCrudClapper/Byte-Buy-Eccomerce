using ByteBuy.Core.Domain.Entities;
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

    public static Result<AddressValueObj> Create(string street, string houseNumber,string postalCode, string city, string country, string? flatNumber = null)
    {
        if (string.IsNullOrWhiteSpace(street) || street.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("Street is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(houseNumber) || houseNumber.Length > 20)
            return Result.Failure<AddressValueObj>(Error.Validation("House number is required and must be at most 20 characters."));

        if (string.IsNullOrWhiteSpace(postalCode) || postalCode.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("Postal code is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(city) || city.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("City is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(country) || country.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("Country is required and must be at most 50 characters."));

        if (flatNumber is not null && flatNumber.Length > 10)
            return Result.Failure<AddressValueObj>(Error.Validation("Flat number must be at most 10 characters."));

        return new AddressValueObj(street, houseNumber, postalCode, city, country, flatNumber);
    }

    public static AddressValueObj FromEntity(Address address)
        => new AddressValueObj(
            address.Street,
            address.HouseNumber,
            address.PostalCode,
            address.City,
            address.Country.Name,
            address.FlatNumber
        );
}

