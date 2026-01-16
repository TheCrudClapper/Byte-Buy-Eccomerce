namespace ByteBuy.Core.DTO.Offer.Common;

public record CompanySellerResponse(
    string CompanyName,
    string CompanyEmail,
    string City,
    string PostalCity,
    string PostalCode,
    string Phone,
    string Tin);
