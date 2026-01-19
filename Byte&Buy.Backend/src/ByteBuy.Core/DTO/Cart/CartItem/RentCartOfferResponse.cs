using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Cart.CartItem;

public record RentCartOfferResponse : CartItemResponse
{
    public MoneyDto PricePerDay { get; init; } = null!;
    public int RentalDays { get; init; }
}
