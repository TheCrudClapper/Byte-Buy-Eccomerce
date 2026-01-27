using ByteBuy.Core.DTO.Public.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Employee;

/// <summary>
/// Represents a request to update an employee's personal and contact information.
/// </summary>
public record EmployeeUpdateRequest(
    [Required] Guid RoleId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required] HomeAddressDto HomeAddress,
    string Password,
    [Required, MaxLength(15)] string PhoneNumber,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds);