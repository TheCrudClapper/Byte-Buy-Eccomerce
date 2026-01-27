namespace ByteBuy.Core.DTO.Public.ApplicationUser;

public record UserBasicInfoResponse(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber);

