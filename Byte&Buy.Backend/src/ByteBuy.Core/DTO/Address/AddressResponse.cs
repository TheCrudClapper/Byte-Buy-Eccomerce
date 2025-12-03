namespace ByteBuy.Core.DTO.Address;

public record AddressResponse(
    Guid Id,
    string Label,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber
    );

