using ByteBuy.Core.Domain.Shared.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Statistics;

public record GMVBySellerTypeQueryModel
{
    public Money CompanyGMV { get; set; } = null!;
    public Money PrivateSellerGMV { get; set; } = null!;
}
