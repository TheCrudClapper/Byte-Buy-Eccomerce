namespace ByteBuy.Services.DTO.Address;

public record AddressAddRequest(
    Guid CountryId,
    string Label,
    string Street,
    string HouseNumber,
    string PostalCode,
    string PostalCity,
    string City,
    string? FlatNumber,
    bool IsDefault
);
