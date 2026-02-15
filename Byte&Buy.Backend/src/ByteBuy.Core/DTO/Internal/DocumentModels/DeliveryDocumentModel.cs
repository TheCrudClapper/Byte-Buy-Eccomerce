namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record DeliveryDocumentModel
{
    public string CarrierCode { get; init; } = null!;
    public string DeliveryName { get; init; } = null!;
    public decimal Total { get; init; }
    public string TotalCurrency { get; init; } = null!;
    public DateTime DeliveredDate { get; init; }
}
