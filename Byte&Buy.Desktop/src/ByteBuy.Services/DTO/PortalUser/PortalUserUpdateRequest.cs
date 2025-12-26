namespace ByteBuy.Services.DTO.PortalUser;

public record PortalUserUpdateRequest(
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string? Password,
    string? PhoneNumber,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
);

