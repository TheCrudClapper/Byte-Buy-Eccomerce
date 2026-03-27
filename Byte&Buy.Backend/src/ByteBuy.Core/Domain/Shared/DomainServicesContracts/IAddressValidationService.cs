using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.DomainServicesContracts;

public interface IAddressValidationService
{
    /// <summary>
    /// Validates common fileds used in ShippingAddress Value Object and ShippingAddress Entity
    /// </summary>
    /// <returns>Returns a Result object that tells if validation went right or wrong</returns>
    Result Validate(
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string? flatNumber);
}
