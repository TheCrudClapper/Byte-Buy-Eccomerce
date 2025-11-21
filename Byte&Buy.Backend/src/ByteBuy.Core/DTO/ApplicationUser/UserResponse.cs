namespace ByteBuy.Core.DTO.User;

public record UserResponse(
    Guid Id,
    string FirstName,
    string Role,
    string LastName,
    string Email,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber
);
