using ByteBuy.Core.DTO.AddressValueObj;

namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserResponse(
    Guid Id,
    Guid RoleId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    HomeAddressDto? HomeAddress,
    IReadOnlyCollection<Guid> GrantedPermissionIds,
    IReadOnlyCollection<Guid> RevokedPermissionIds
    );
