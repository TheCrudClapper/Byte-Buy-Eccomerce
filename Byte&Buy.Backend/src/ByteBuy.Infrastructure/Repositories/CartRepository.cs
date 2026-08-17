using ByteBuy.Core.Domain.Carts;
using ByteBuy.Core.DTO.Internal.Checkout;

namespace ByteBuy.Infrastructure.Repositories;

public class CartRepository : EfBaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<CheckoutItemQueryModel>> GetCartOffersAsCheckoutItemQuery(Guid userId, CancellationToken ct = default)
    {
        return await _context.CartOffers
          .AsNoTracking()
          .Where(item => item.Cart.UserId == userId)
          .Select(CheckoutMappings.CheckoutItemQueryProjection)
          .ToListAsync(ct);
    }

}
