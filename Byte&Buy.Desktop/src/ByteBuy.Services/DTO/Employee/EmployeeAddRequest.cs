namespace ByteBuy.Services.DTO.Employee;

public record EmployeeAddRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber
);