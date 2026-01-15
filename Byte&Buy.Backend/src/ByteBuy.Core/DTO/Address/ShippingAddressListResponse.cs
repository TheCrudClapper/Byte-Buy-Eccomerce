namespace ByteBuy.Core.DTO.Address;

public record ShippingAddressListResponse(
    Guid Id,
    string Label,
    string HouseNumber,
    string PostalCity,
    string PostalCode,
    string? FlatNumber,
    string City,
    bool IsDefault
    );
