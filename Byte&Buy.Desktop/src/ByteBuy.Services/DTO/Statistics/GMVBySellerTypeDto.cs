using ByteBuy.Services.DTO.Money;

namespace ByteBuy.Services.DTO.Statistics;

public record GMVBySellerTypeDto
{
    public string Display { get; set; } = null!;
    public MoneyDto GMVAmount { get; set; } = null!;
}
