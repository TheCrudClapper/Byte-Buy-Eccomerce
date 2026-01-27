using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Cart.CartOffer;

public record RentCartOfferResponse : CartOfferResponse
{
    public MoneyDto PricePerDay { get; init; } = null!;
    public int RentalDays { get; init; }
}
