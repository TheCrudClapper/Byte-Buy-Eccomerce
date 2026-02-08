using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.Cart;
using ByteBuy.Core.DTO.Internal.Checkout;
using ByteBuy.Core.Mappings;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CartRepository : EfBaseRepository<Cart>, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<CheckoutItemQuery>> GetCartOffersAsCheckoutItemQuery(Guid userId, CancellationToken ct = default)
    {
        return await _context.CartOffers
          .AsNoTracking()
          .Where(item => item.Cart.UserId == userId)
          .Select(CheckoutMappings.CheckoutItemQueryProjection)
          .ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<FlatCartOffersQuery>> GetCartOffersForCheckout(Guid userId, CancellationToken ct = default)
    {
        return await _context.CartOffers
            .AsNoTracking()
            .Where(item => item.Cart.UserId == userId)
            .Select(CartMappings.FlatCartOffersProjection)
            .ToListAsync(ct);
    }
}
