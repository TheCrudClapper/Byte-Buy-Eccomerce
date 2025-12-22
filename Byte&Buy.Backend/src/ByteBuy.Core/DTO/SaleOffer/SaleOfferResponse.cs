namespace ByteBuy.Core.DTO.SaleOffer;

public record SaleOfferResponse(
    Guid Id,
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerItem,
    IEnumerable<Guid>? ParcelLockerDeliveries,
    IEnumerable<Guid> OtherDeliveriesIds);
