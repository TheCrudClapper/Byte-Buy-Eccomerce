using ByteBuy.Core.DTO.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Employee;
/// <summary>
/// Represents a request to add a new employee, including personal, contact, and address information required for
/// registration.
public record EmployeeAddRequest(
    [Required] Guid RoleId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required, MaxLength(15)] string PhoneNumber,
    [Required] HomeAddressDto HomeAddress,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
    );
