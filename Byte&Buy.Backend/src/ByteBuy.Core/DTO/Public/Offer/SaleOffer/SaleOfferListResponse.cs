namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferListResponse(Guid Id,
    string ItemName,
    int QuantityAvailable,
    string CreatorEmail,
    decimal PricePerItem,
    string Currency);
