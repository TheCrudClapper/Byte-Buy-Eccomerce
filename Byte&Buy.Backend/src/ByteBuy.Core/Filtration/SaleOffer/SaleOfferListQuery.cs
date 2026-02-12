using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.SaleOffer;

public sealed class SaleOfferListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;
    public string? Name { get; init; }
    public decimal? PriceFrom { get; init; }
    public decimal? PriceTo { get; init; }
    public int? QuantityFrom { get; init; }
    public int? QuantityTo { get; init; }
}
