namespace ByteBuy.Core.DTO.Internal.Company;

public record CompanyAddressWithIdQueryModel(
    Guid Id,
    AddressValueObject CompanyAddress);
