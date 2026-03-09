using Ardalis.Specification;
using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.DTO.Internal.Offer;
using ByteBuy.Core.Mappings;

namespace ByteBuy.Core.Specification;

public static class OfferSpecifications
{
    public sealed class UserOffersPanelSpec : Specification<Offer, UserPanelOfferQueryModel>
    {
        public UserOffersPanelSpec(Guid userId)
        {
            Query.AsNoTracking()
                .OrderByDescending(o => o.DateCreated)
                .Where(o => o.CreatedByUserId == userId)
                .Select(OfferMappings.UserPanelOfferQueryProjection);

        }
    }
}
