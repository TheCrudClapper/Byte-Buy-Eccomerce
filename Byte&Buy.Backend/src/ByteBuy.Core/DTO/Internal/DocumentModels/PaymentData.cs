using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record PaymentData
{
    public Guid PaymentId { get; init; }
    public decimal Total { get; init; }
    public string TotalCurrency { get; init; } = null!;
    public string Method { get; init; } = null!;
    public DateTime DateCreated { get; init; }
}
