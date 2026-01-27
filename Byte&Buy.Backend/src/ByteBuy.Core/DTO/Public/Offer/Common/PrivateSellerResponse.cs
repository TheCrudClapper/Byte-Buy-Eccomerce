namespace ByteBuy.Core.DTO.Public.Offer.Common;

public record PrivateSellerResponse(
    string FirstName,
    string Email,
    string City,
    string PostalCity,
    string PostalCode,
    string? Phone,
    DateTime AccountCreatedDate);
