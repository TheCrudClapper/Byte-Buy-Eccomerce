using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class SaleOfferSpecifications
{
    public sealed class SaleOfferToSaleOfferListResponseSpec : Specification<SaleOffer, SaleOfferListResponse>
    {
        public SaleOfferToSaleOfferListResponseSpec()
        {
            Query.Select(SaleOfferMappings.SaleOfferListProjection);
        }
    }

    public sealed class SaleOfferToSaleOfferResponseSpec : Specification<SaleOffer, SaleOfferResponse>
    {
        public SaleOfferToSaleOfferResponseSpec(Guid id)
        {
            Query.Where(so => so.Id == id)
                .Select(SaleOfferMappings.SaleOfferResponseProjection);
        }
    }
}
