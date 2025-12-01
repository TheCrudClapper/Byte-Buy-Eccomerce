namespace ByteBuy.Services.DTO.Employee;

/// <summary>
/// Represents a request to update an employee's personal and contact information.
/// </summary>
public record EmployeeUpdateRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? PhoneNumber,
    string? FlatNumber,
    string Password
    );