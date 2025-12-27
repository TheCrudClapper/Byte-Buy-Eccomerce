namespace ByteBuy.Services.DTO.SaleOffer;

public record SaleOfferResponse(
    Guid Id,
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerItem,
    IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
    IReadOnlyCollection<Guid> OtherDeliveriesIds);
