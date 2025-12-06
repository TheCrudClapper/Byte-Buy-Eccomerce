using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.PortalUser;

public record PortalUserUpdateRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string? Password,
    string? PhoneNumber,
    UserAddressUpdateRequest? Address,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
);

