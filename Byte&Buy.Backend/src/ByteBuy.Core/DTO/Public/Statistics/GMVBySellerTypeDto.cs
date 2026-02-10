using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Public.Statistics;

public record GMVBySellerTypeDto
{
    public string Display { get; set; } = null!;
    public MoneyDto GMVAmount { get; set; } = null!;
}
