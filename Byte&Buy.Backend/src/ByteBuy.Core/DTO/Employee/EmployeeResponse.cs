namespace ByteBuy.Core.DTO.Employee;

/// <summary>
/// Represents the response data for an employee, including personal and address information.
/// </summary>
public record EmployeeResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber,
    string? PhoneNumber,
    IReadOnlyCollection<Guid> GrantedPermissionIds,
    IReadOnlyCollection<Guid> RevokedPermissionIds
);
