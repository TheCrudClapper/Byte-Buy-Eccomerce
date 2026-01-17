namespace ByteBuy.Core.DTO.Offer.Common;

public record PrivateSellerResponse(
    string FirstName,
    string Email,
    string City,
    string PostalCity,
    string PostalCode,
    string? Phone,
    DateTime AccountCreatedDate);
