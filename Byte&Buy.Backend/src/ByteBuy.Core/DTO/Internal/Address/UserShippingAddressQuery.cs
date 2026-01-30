namespace ByteBuy.Core.DTO.Internal.Address;

public record UserShippingAddressQuery(
    Guid Id,
    string Street,
    string City,
    string PostalCity,
    string PostalCode,
    string HouseNumber,
    string? FlatNumber);