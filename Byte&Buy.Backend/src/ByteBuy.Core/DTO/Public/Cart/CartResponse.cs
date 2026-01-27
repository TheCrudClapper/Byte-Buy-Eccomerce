using ByteBuy.Core.DTO.Public.Cart.CartOffer;
namespace ByteBuy.Core.DTO.Public.Cart;

public record CartResponse(
    IReadOnlyCollection<CartOfferResponse> CartOffers,
    CartSummaryResponse Summary);
