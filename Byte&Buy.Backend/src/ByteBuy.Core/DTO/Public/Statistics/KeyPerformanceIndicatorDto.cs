namespace ByteBuy.Core.DTO.Public.Statistics;

public record KeyPerformanceIndicatorDto
{
    public string Key { get; init; } = null!;
    public string Label { get; init; } = null!;

    public decimal Value { get; init; }
    public string DisplayValue { get; init; } = null!;
}
