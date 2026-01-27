using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class RentOfferSpecifications
{
    public sealed class RentOfferToRentOfferListResponseSpec : Specification<RentOffer, RentOfferListResponse>
    {
        public RentOfferToRentOfferListResponseSpec()
        {
            Query.AsNoTracking()
                 .Where(ro => ro.Item.IsCompanyItem)
                 .Select(RentOfferMappings.RentOfferListProjection);
        }
    }

    public sealed class RentOfferToRentOfferResponseSpec : Specification<RentOffer, RentOfferResponse>
    {
        public RentOfferToRentOfferResponseSpec(Guid id)
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

    public sealed class UserRentOfferToResponseSpec : Specification<RentOffer, UserRentOfferResponse>
    {
        public UserRentOfferToResponseSpec(Guid userId, Guid id)
        {
            Query.Where(ro => ro.Id == id && ro.CreatedByUserId == userId)
                .Select(RentOfferMappings.UserRentOfferResponseProjection);
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

    public sealed class RentOfferDetailsSpec : Specification<RentOffer, RentOfferDetailsResponse>
    {
        public RentOfferDetailsSpec(Guid id)
        {
            Query
                .AsNoTracking()
                .Where(ro => ro.Id == id)
                .Select(RentOfferMappings.RentOfferDetailsResponseProjection);
        }
    }


}
