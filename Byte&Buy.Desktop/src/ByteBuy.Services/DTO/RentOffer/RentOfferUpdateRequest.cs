namespace ByteBuy.Services.DTO.RentOffer;

public record RentOfferUpdateRequest(
    int AdditionalQuantity,
    decimal PricePerDay,
    int AdditionalRentalDays,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    IEnumerable<Guid> OtherDeliveriesIds);
