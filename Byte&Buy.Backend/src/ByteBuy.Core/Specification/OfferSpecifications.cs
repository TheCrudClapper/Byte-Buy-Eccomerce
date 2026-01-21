using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Specification;

public static class OfferSpecifications
{
    public sealed class OfferBrowserSpec : Specification<Offer>
    {
        public OfferBrowserSpec()
        {
            Query
                .AsNoTracking()
                .Include(o => o.Item)
                    .ThenInclude(o => o.Category)
                .Include(o => o.Item)
                    .ThenInclude(o => o.Condition)
                 .Include(o => o.Item)
                    .ThenInclude(o => o.Images);
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
