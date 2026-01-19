using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Specification;

public static class CartSpecifications
{
    public sealed class CartAggregateWithOffersByUserIdSpec : Specification<Cart>
    {
        public CartAggregateWithOffersByUserIdSpec(Guid userId)
        {
            Query
                .Where(c => c.UserId == userId)
                .Include(c => c.CartOffers)
                .ThenInclude(co => co.Offer);
        }
    }

    public sealed class CartAggregateByUserIdSpec : Specification<Cart>
    {
        public CartAggregateByUserIdSpec(Guid userId, bool includeOffer = false)
        {
            if (includeOffer)
            {
                Query.Where(c => c.UserId == userId)
                     .Include(c => c.CartOffers)
                     .ThenInclude(c => c.Offer);
            }
            else
            {
                Query.Where(c => c.UserId == userId)
                    .Include(c => c.CartOffers);
            }
        }
    }

    public sealed class CartAggegateWithFullOffer : Specification<Cart>
    {
        public CartAggegateWithFullOffer(Guid userId)
        {
            Query.Where(c => c.UserId == userId)
                .AsSplitQuery()
                .Include(c => c.CartOffers)
                .ThenInclude(c => c.Offer)
                    .ThenInclude(o => o.Item)
                        .ThenInclude(i => i.Images);
        }
    }
}
