using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.DomainServices;

public class AddressValidationService : IAddressValidationService
{
    public Result Validate(string street, string houseNumber, string postalCode, string city, string? flatNumber)
    {
        if (string.IsNullOrWhiteSpace(street) || street.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("Street is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(houseNumber) || houseNumber.Length > 20)
            return Result.Failure<AddressValueObj>(Error.Validation("House number is required and must be at most 20 characters."));

        if (string.IsNullOrWhiteSpace(postalCode) || postalCode.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("Postal code is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(city) || city.Length > 50)
            return Result.Failure<AddressValueObj>(Error.Validation("City is required and must be at most 50 characters."));

        if (flatNumber is not null && flatNumber.Length > 10)
            return Result.Failure<AddressValueObj>(Error.Validation("Flat number must be at most 10 characters."));

        return Result.Success();
    }
}
