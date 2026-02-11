using ByteBuy.Core.DTO.Public.Money;

namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record PaymentData
{
    public Guid PaymentId { get; init; }
    public string Total { get; init; } = null!;
    public DateTime DateCreated { get; init; }
}
