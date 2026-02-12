using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.Filtration.DeliveryCarrier;

public sealed class DeliveryCarriersListQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Page number must greater that 0")]
    public int PageNumber { get; init; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must greater that 0")]
    public int PageSize { get; init; } = 10;

    public string? DeliveryCarrierName { get; init; }
    public string? Code { get; init; }
}
