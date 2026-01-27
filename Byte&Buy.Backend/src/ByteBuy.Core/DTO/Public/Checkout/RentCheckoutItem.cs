using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Checkout;

public record RentCheckoutItem : CheckoutItem
{
    public int RentalDays { get; init; }
    public MoneyDto PricePerDay { get; init; } = null!;

}