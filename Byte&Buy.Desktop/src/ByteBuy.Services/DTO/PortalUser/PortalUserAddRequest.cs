using System.ComponentModel.DataAnnotations;
using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.PortalUser;

public record PortalUserAddRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber,
    AddressAddRequest Address,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
);