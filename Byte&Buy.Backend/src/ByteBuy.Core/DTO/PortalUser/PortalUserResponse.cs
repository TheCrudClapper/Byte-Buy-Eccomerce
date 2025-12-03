namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserResponse(
    Guid Id,
    Guid RoleId,
    Guid CountryId,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string HouseNumber,
    string PostalCode,
    string City,
    string Country,
    string? FlatNumber,
    string? PhoneNumber,
    IEnumerable<Guid> GrantedPermissionIds,
    IEnumerable<Guid> RevokedPermissionIds
    );
