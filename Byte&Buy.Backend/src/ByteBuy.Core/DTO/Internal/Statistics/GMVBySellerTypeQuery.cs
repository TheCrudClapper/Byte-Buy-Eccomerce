using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Statistics;

public record GMVBySellerTypeQuery
{
    public Money CompanyGMV { get; set; } = null!;
    public Money PrivateSellerGMV { get; set; } = null!;
}
