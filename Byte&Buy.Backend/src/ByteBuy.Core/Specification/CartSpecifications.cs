using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Specification;

public static class CartSpecifications
{
    public sealed class UserCartAggregateAndOffersSpec : Specification<Cart>
    {
        public UserCartAggregateAndOffersSpec(Guid userId)
        {
            Query
                .Where(c => c.UserId == userId)
                .Include(c => c.CartOffers)
                .ThenInclude(co => co.Offer);
        }
    }

    public sealed class UserCartAggregateSpec : Specification<Cart>
    {
        public UserCartAggregateSpec(Guid userId, bool includeOffer = false)
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

    public sealed class UserCartAggegateWithOffersAggregateSpec : Specification<Cart>
    {
        public UserCartAggegateWithOffersAggregateSpec(Guid userId)
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
