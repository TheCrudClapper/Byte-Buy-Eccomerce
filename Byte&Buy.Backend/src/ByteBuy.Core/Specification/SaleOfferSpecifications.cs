using Ardalis.Specification;
using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class SaleOfferSpecifications
{
    public sealed class SaleOfferResponseSpec : Specification<SaleOffer, SaleOfferResponse>
    {
        public SaleOfferResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(so => so.Id == id)
                 .Select(SaleOfferMappings.SaleOfferResponseProjection);
        }
    }

    public sealed class SaleOfferAggregateSpec : Specification<SaleOffer>
    {
        public SaleOfferAggregateSpec(Guid id, bool ignoreQueryFilters = true)
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

    public sealed class UserSaleOffeResponseSpec : Specification<SaleOffer, UserSaleOfferResponse>
    {
        public UserSaleOffeResponseSpec(Guid userId, Guid id)
        {
            Query
                .AsNoTracking()
                .Where(so => so.CreatedByUserId == userId && so.Id == id)
                .Select(SaleOfferMappings.UserSaleOfferResponseProjection);
        }
    }

    public sealed class SaleOfferDetailsResponseSpec : Specification<SaleOffer, SaleOfferDetailsResponse>
    {
        public SaleOfferDetailsResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                .Where(so => so.Id == id)
                .Select(SaleOfferMappings.SaleOfferDetailsResponseProjection);
        }
    }
}
