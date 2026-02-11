namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderDetailsPdfModel
{
    public CompanyData CompanyData { get; init; } = null!;
    public CustomerData CustomerData { get; init; } = null!;
    public PaymentData PaymentData { get; init; } = null!;
    public DeliveryData DeliveryData { get; init; } = null!;

    public OrderData OrderData { get; init; } = null!;
}
