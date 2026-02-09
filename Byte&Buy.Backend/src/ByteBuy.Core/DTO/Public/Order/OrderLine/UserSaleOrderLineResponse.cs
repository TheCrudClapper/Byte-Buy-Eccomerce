using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Order.OrderLine;

public record UserSaleOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerItem { get; set; } = null!;
}
