using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Order.OrderLine;

public record UserRentOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerDay { get; set; } = null!;
    public int RentalDays { get; set; }
}
