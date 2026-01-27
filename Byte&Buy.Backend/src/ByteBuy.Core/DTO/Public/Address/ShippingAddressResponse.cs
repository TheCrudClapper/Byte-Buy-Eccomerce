namespace ByteBuy.Core.DTO.Public.Address;

public record ShippingAddressResponse(
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

