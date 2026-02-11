namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record DeliveryData
{
    public string CarrierCode { get; init; } = null!;
    public string DeliveryName { get; init; } = null!;
    public string Total { get; init; } = null!;
    public DateTime DeliveredDate { get; init; }
}
