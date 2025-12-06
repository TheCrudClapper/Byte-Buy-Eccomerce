namespace ByteBuy.Core.DTO.PortalUser;

public record AddressResponse(
    Guid Id,
    Guid CountryId,
    string Label,
    string Street,
    string HouseNumber,
    string PostalCity,
    string PostalCode,
    string City,
    string? FlatNumber,
    bool IsDefault
    );

