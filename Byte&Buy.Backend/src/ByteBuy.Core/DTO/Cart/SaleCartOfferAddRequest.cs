namespace ByteBuy.Core.DTO.Cart;

public record SaleCartOfferAddRequest(
    int Quantity,
    Guid OfferId);
