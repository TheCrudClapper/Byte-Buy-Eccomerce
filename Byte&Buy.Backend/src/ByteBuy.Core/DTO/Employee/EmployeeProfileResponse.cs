namespace ByteBuy.Core.DTO.Employee;

public record EmployeeProfileResponse(
    Guid Id,
    string RoleName,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber,
    string? PhoneNumber
);

