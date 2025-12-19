namespace ByteBuy.Services.DTO.SaleOffer;

public record SaleOfferAddRequest(
    Guid ItemId,
    int QuantityAvailable,
    decimal PricePerItem,
    IEnumerable<Guid> SelectedDeliveriesIds);
