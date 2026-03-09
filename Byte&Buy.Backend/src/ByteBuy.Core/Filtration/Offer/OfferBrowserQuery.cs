using ByteBuy.Core.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Offer;

public sealed class OfferBrowserQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;

    public OfferSortBy SortBy { get; init; } = OfferSortBy.Newest;

    public IReadOnlyCollection<Guid>? CategoryIds { get; init; }
    public IReadOnlyCollection<Guid>? ConditionIds { get; init; }
    public SellerType? SellerType { get; init; }
    public string? SearchPhrase { get; init; }
    public string? City { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public int? MinRentalDays { get; init; }
    public int? MaxRentalDays { get; init; }
}
