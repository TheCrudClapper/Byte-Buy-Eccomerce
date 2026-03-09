using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.Domain.Shared.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.DomainServices;

public class AddressValidationService : IAddressValidationService
{
    public Result Validate(string street, string houseNumber, string postalCity, string postalCode, string city, string? flatNumber)
    {
        if (string.IsNullOrWhiteSpace(street) || street.Length > 50)
            return Result.Failure<AddressValueObject>(AddressErrors.StreetInvalid);

        if (string.IsNullOrWhiteSpace(houseNumber) || houseNumber.Length > 20)
            return Result.Failure<AddressValueObject>(AddressErrors.HouseNumberInvalid);

        if (string.IsNullOrWhiteSpace(postalCode) || postalCode.Length > 20)
            return Result.Failure<AddressValueObject>(AddressErrors.PostalCodeInvalid);

        if (string.IsNullOrWhiteSpace(postalCity) || postalCity.Length > 50)
            return Result.Failure<AddressValueObject>(AddressErrors.PostalCityInvalid);

        if (string.IsNullOrWhiteSpace(city) || city.Length > 50)
            return Result.Failure<AddressValueObject>(AddressErrors.CityInvalid);

        if (flatNumber is not null && flatNumber.Length > 10)
            return Result.Failure<AddressValueObject>(AddressErrors.FlatNumberInvalid);

        return Result.Success();
    }
}
