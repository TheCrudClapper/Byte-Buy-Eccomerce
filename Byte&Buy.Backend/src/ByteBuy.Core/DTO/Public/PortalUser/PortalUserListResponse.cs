namespace ByteBuy.Core.DTO.Public.PortalUser;

public record PortalUserListResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
);

