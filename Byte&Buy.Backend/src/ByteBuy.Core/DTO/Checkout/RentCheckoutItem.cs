using ByteBuy.Core.DTO.Money;

namespace ByteBuy.Core.DTO.Checkout;

public record RentCheckoutItem : CheckoutItem
{
    public int RentalDays { get; init; }
    public MoneyDto PricePerDay { get; init; } = null!;

}