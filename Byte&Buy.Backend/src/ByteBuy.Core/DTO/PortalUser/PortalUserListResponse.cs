namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserListResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Role
);

