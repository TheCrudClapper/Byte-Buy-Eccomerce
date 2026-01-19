using ByteBuy.Core.DTO.Cart.CartItem;
namespace ByteBuy.Core.DTO.Cart;

public record CartResponse(
    IReadOnlyCollection<CartOfferResponse> CartOffers,
    CartSummaryResponse Summary);
