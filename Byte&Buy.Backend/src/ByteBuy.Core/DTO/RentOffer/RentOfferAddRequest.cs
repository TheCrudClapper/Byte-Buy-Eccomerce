namespace ByteBuy.Core.DTO.RentOffer;

public record RentOfferAddRequest(
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerDay,
    int MaxRentalDays,
    IEnumerable<Guid> SelectedDeliveriesIds);
