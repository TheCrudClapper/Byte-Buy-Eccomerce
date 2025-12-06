using ByteBuy.Core.DTO.PortalUser;

namespace ByteBuy.Services.DTO.PortalUser;

public record PortalUserAddRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string? PhoneNumber,
    UserAddressAddRequest Address,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
);