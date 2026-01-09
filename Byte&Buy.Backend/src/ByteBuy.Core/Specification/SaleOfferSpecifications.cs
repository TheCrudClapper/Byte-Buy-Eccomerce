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

            Query.Where(so => so.Id == id)
                 .Include(so => so.OfferDeliveries);
        }
    }

    public sealed class UserSaleOfferAggregateSpec : Specification<SaleOffer>
    {
        public UserSaleOfferAggregateSpec(Guid userId, Guid id, bool ignoreQueryFilters = true)
        {
            if (ignoreQueryFilters)
                Query.IgnoreQueryFilters();

            Query.Where(so => so.CreatedByUserId == userId && so.Id == id)
                .Include(so => so.OfferDeliveries);
        }
    }

    public sealed class UserSaleOfferAsResponseDtoSpec : Specification<SaleOffer, UserSaleOfferResponse>
    {
        public UserSaleOfferAsResponseDtoSpec(Guid userId, Guid id)
        {
            Query
                .AsNoTracking()
                .Where(so => so.CreatedByUserId == userId && so.Id == id)
                .Select(SaleOfferMappings.UserSaleOfferResponseProjection);
        }
    }
}
