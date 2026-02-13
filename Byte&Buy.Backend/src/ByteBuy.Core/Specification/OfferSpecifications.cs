using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Internal.Offer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OfferSpecifications
{
    public sealed class OfferBrowserSpec : Specification<Offer, OfferBrowserItemQuery>
    {
        public OfferBrowserSpec()
        {
            Query.AsNoTracking()
                 .Select(OfferMappings.OfferBrowserItemQueryProjection);
        }
    }

    public sealed class UserOffersPanelSpec : Specification<Offer, UserPanelOfferQuery>
    {
        public UserOffersPanelSpec(Guid userId)
        {
            Query.AsNoTracking()
                .OrderByDescending(o => o.DateCreated)
                .Where(o => o.CreatedByUserId == userId)
                .Select(OfferMappings.UserOfferPanelQueryProjection);

        }
    }
}
