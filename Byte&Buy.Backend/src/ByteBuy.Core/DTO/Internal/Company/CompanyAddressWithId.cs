using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Company;

public record CompanyAddressWithId(
    Guid Id,
    AddressValueObject CompanyAddress);
