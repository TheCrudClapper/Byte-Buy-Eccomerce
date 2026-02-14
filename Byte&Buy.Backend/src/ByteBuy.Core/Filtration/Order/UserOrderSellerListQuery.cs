using ByteBuy.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Order;

public sealed class UserOrderSellerListQuery
{

    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;
    public string? BuyerFullName { get; init; }
    public string? ItemName { get; init; }
    public OrderStatus? Status { get; init; }
    public DateTime? PurchasedFrom { get; init; }
    public DateTime? PurchasedTo { get; init; }

}
