namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    AddressResponse? Address,
    IEnumerable<Guid> GrantedPermissionIds,
    IEnumerable<Guid> RevokedPermissionIds
    );
