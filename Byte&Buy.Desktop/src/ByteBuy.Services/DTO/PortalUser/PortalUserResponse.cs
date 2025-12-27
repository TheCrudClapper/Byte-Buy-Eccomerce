using ByteBuy.Services.DTO.Address;

namespace ByteBuy.Services.DTO.PortalUser;

public record PortalUserResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    AddressResponse? Address,
    IReadOnlyCollection<Guid> GrantedPermissionIds,
    IReadOnlyCollection<Guid> RevokedPermissionIds
);
