namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record RentOfferListResponse(
    Guid Id,
    string ItemName,
    int QuantityAvailable,
    string CreatorEmail,
    string Currency,
    decimal Amount,
    int MaxRentalDays);
