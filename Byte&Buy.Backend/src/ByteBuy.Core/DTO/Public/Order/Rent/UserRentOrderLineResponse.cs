using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Common;

namespace ByteBuy.Core.DTO.Public.Order.Rent;

public record UserRentOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerDay { get; set; } = null!;
    public int RentalDays { get; set; }
}
