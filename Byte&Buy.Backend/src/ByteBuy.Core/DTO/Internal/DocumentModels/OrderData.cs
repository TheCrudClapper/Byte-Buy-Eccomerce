namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderData
{
    public Guid OrderId { get; init; }
    public DateTime DateCreated { get; init; }
    public string Total { get; init; } = null!;
    public IReadOnlyCollection<OrderLineData> Lines { get; init; } = [];
}
