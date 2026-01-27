namespace ByteBuy.Core.DTO.Public.PortalUser;

public record UserBasicInfoUpdateRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
