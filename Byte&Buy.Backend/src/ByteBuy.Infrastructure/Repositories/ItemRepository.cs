using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class ItemRepository : EfBaseRepository<Item>, IItemRepository
{
    public ItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Item?> GetAggregateAsync(Guid itemId, CancellationToken ct = default)
    {
        return await _context.Items
           .Include(i => i.Images)
           .FirstOrDefaultAsync(i => i.Id == itemId, ct);
    }

}
