using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;

namespace ByteBuy.Infrastructure.Repositories;

public class CartRepository : BaseRepository, ICartRepository
{
    public CartRepository(ApplicationDbContext context) : base(context){}

    public async Task AddCart(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
    }
}
