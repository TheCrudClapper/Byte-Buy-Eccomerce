using ByteBuy.Core.DTO.Cart.CartItem;
using System;
namespace ByteBuy.Core.DTO.Cart;

public record CartResponse(
    IReadOnlyCollection<CartItemResponse> CartItems,
    CartSummaryResponse Summary);
