using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class SaleOfferSpecifications
{
    public sealed class SaleOfferToSaleOfferListResponseSpec : Specification<SaleOffer, SaleOfferListResponse>
    {
        public SaleOfferToSaleOfferListResponseSpec()
        {
            Query.AsNoTracking()
                 .Select(SaleOfferMappings.SaleOfferListProjection);
        }
    }

    public sealed class SaleOfferToSaleOfferResponseSpec : Specification<SaleOffer, SaleOfferResponse>
    {
        public SaleOfferToSaleOfferResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(so => so.Id == id)
                 .Select(SaleOfferMappings.SaleOfferResponseProjection);
        }
    }

    public sealed class SaleOfferWithOfferDeliveriesSpec : Specification<SaleOffer>
    {
        public SaleOfferWithOfferDeliveriesSpec(Guid id, bool ignoreQueryFilters = true)
        {
            if (ignoreQueryFilters)
                Query.IgnoreQueryFilters();

            Query.Where(ro => ro.Id == id)
                 .Include(ro => ro.OfferDeliveries);
        }
    }
}
