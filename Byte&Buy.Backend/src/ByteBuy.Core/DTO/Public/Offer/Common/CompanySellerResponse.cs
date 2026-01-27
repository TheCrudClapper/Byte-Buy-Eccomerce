namespace ByteBuy.Core.DTO.Public.Offer.Common;

public record CompanySellerResponse(
    string CompanyName,
    string CompanyEmail,
    string City,
    string PostalCity,
    string PostalCode,
    string Phone,
    string Tin);
