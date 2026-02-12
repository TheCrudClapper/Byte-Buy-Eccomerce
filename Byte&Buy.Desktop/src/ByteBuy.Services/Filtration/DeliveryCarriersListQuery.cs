namespace ByteBuy.Services.Filtration;

public sealed class DeliveryCarriersListQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 11;
    public string? DeliveryCarrierName { get; init; }
    public string? Code { get; init; }
}
