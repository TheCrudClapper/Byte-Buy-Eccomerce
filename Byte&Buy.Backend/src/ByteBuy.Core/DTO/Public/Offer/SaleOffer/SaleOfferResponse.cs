namespace ByteBuy.Core.DTO.Public.Offer.SaleOffer;

public record SaleOfferResponse(
    Guid Id,
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerItem,
    IReadOnlyCollection<Guid>? ParcelLockerDeliveries,
    IReadOnlyCollection<Guid> OtherDeliveriesIds);
