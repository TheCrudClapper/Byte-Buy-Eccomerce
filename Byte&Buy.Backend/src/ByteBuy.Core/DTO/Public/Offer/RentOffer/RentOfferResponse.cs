namespace ByteBuy.Core.DTO.Public.Offer.RentOffer;

public record RentOfferResponse(
    Guid Id,
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerDay,
    int MaxRentalDays,
    IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
    IReadOnlyCollection<Guid> OtherDeliveriesIds);
