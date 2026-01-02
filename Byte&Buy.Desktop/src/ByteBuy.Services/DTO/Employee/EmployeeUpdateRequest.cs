using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.Employee;

/// <summary>
/// Represents a request to update an employee's personal and contact information.
/// </summary>
public record EmployeeUpdateRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    HomeAddressDto HomeAddress,
    string Password,
    string PhoneNumber,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds);