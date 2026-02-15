using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderDetailsPdfModel
{
    public OrderStatus OrderStatus { get; set; }
    public CompanyDocumentModel CompanyData { get; init; } = null!;
    public CustomerDocumentModel CustomerData { get; init; } = null!;
    public PaymentDocumentModel PaymentData { get; init; } = null!;
    public DeliveryDocumentModel DeliveryData { get; init; } = null!;
    public OrderDocumentModel OrderData { get; init; } = null!;
}
