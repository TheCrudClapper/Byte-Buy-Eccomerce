using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.DomainServicesContracts;

public interface IAddressValidationService
{
    /// <summary>
    /// Validates common fileds used in Address Value Object and Address Entity
    /// </summary>
    /// <returns>Returns a Result object that tells if validation went right or wrong</returns>
    Result Validate(
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string? flatNumber);
}
