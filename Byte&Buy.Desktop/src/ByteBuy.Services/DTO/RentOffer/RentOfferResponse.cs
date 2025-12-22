namespace ByteBuy.Services.DTO.RentOffer;

public record RentOfferResponse(
    Guid Id,
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerDay,
    int MaxRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    IEnumerable<Guid> OtherDeliveriesIds);
