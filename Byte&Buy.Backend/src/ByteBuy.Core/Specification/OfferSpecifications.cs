using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.DTO.Offer.Common;
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

    public sealed class UserOffersPanelSpec : Specification<Offer>
    {
        public UserOffersPanelSpec(Guid userId)
        {
            Query.AsNoTracking()
                .Where(o => o.CreatedByUserId == userId)
                .Include(o => o.Item)
                    .ThenInclude(o => o.Images);
        }
    }
}
