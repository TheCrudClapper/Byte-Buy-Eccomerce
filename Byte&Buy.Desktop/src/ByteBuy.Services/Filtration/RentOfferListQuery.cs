namespace ByteBuy.Services.Filtration;

public sealed class RentOfferListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? Name { get; init; }
    public decimal? PriceFrom { get; init; }
    public decimal? PriceTo { get; init; }
    public int? MaxRentalDaysFrom { get; init; }
    public int? MaxRentalDaysTo { get; init; }
    public int? QuantityFrom { get; init; }
    public int? QuantityTo { get; init; }
}
