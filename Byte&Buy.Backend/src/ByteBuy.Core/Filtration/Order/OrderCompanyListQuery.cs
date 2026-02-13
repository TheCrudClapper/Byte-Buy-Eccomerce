using ByteBuy.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Order;

public class OrderCompanyListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;

    public string? BuyerFullName { get; init; }
    public string? BuyerEmail { get; init; }
    public DateTime? PurchasedFrom { get; init; }
    public DateTime? PurchasedTo { get; init; }
    public decimal? TotalFrom { get; init; }
    public decimal? TotalTo { get; init; }
}
