namespace ByteBuy.Core.DTO.Public.Address;

public record ShippingAddressCheckout(
    Guid Id,
    string Label,
    string Street,
    string PostalCity,
    string PostalCode,
    string City,
    string HouseNumber,
    string? FlatNumber,
    bool IsDefault
    );
