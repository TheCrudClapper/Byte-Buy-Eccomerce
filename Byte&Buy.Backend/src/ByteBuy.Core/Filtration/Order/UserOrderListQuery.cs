using ByteBuy.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.Order;

public sealed class UserOrderListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;
}
