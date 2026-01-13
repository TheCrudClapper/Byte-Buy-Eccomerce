namespace ByteBuy.Core.DTO.PortalUser;

public record UserBasicInfoUpdateRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
