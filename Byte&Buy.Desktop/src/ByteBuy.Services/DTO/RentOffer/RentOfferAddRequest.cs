namespace ByteBuy.Services.DTO.RentOffer;

public record RentOfferAddRequest(
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerDay,
    int MaxRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    IEnumerable<Guid> OtherDeliveriesIds);