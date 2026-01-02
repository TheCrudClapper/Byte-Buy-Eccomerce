using ByteBuy.Services.DTO.Address;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Services.DTO.Employee;

public record EmployeeAddRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    HomeAddressDto HomeAddress,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
);