namespace ByteBuy.Core.DTO.Public.Address;

public record ShippingAddressListResponse(
    Guid Id,
    string Label,
    string HouseNumber,
    string PostalCity,
    string PostalCode,
    string? FlatNumber,
    string City,
    bool IsDefault);
