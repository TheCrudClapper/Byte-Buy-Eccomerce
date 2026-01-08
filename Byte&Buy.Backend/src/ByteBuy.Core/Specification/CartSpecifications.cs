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
        public CartAggregateByUserIdSpec(Guid userId)
        {
            Query.Where(c => c.UserId == userId)
                .Include(c => c.CartOffers);
        }
    }
}
