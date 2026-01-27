using ByteBuy.Core.DTO.Public.AddressValueObj;

namespace ByteBuy.Core.DTO.Public.Employee;

/// <summary>
/// Represents the response data for an employee, including personal and address information.
/// </summary>
public record EmployeeResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    HomeAddressDto HomeAddress,
    string PhoneNumber,
    IReadOnlyCollection<Guid> GrantedPermissionIds,
    IReadOnlyCollection<Guid> RevokedPermissionIds
);
