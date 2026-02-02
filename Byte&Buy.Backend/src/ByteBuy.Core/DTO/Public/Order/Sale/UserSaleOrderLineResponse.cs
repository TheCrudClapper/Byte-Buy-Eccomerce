using ByteBuy.Core.DTO.Public.Money;
using ByteBuy.Core.DTO.Public.Order.Common;

namespace ByteBuy.Core.DTO.Public.Order.Sale;
public record UserSaleOrderLineResponse : UserOrderLineResponse
{
    public MoneyDto PricePerItem { get; set; } = null!;
}
