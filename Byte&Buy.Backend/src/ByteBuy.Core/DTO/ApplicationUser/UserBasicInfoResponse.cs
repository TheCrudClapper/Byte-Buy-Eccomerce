namespace ByteBuy.Core.DTO.ApplicationUser;

public record UserBasicInfoResponse(
    string FirstName,
    string LastName,
    string Email,
    string? Phone);

