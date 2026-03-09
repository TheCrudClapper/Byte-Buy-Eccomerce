using Ardalis.Specification;
using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentOfferSpecifications
{
    public sealed class RentOfferResponseSpec : Specification<RentOffer, RentOfferResponse>
    {
        public RentOfferResponseSpec(Guid id)
        {
            Query.AsNoTracking()
                 .Where(ro => ro.Id == id)
                 .Select(RentOfferMappings.RentOfferResponseProjection);
        }
    }

    public sealed class UserRentOfferAggregateSpec : Specification<RentOffer>
    {
        public UserRentOfferAggregateSpec(Guid userId, Guid id)
        {
            Query.Where(ro => ro.Id == id && ro.CreatedByUserId == userId)
                .Include(ro => ro.OfferDeliveries);
        }
    }

    public sealed class RentOfferAggregateSpec : Specification<RentOffer>
    {
        public RentOfferAggregateSpec(Guid id, bool ignoreQueryFilters = true)
        {
            if (ignoreQueryFilters)
                Query.IgnoreQueryFilters();

            Query.Where(ro => ro.Id == id)
                 .Include(ro => ro.OfferDeliveries);
        }
    }

    public sealed class UserRentOfferResponseSpec : Specification<RentOffer, UserRentOfferResponse>
    {
        public UserRentOfferResponseSpec(Guid userId, Guid id)
        {
            Query.Where(ro => ro.Id == id && ro.CreatedByUserId == userId)
                .Select(RentOfferMappings.UserRentOfferResponseProjection);
        }
    }

    public sealed class RentOfferDetailsResponseSpec : Specification<RentOffer, RentOfferDetailsResponse>
    {
        public RentOfferDetailsResponseSpec(Guid id)
        {
            Query
                .AsNoTracking()
                .Where(ro => ro.Id == id)
                .Select(RentOfferMappings.RentOfferDetailsResponseProjection);
        }
    }

}
