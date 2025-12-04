namespace ByteBuy.Core.DTO.Address;

public record AddressResponse(
    Guid Id,
    Guid CountryId,
    string Label,
    string Street,
    string HouseNumber,
    string PostalCity,
    string PostalCode,
    string City,
    string? FlatNumber
    );

