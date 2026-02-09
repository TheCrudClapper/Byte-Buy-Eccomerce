using Ardalis.Specification.EntityFrameworkCore;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class OfferRepository : EfBaseRepository<Offer>, IOfferRepository
{
    public OfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IReadOnlyCollection<Offer>> GetOffersByIdsAsync(IEnumerable<Guid> offerIds, CancellationToken ct = default)
    {
        return await _context.Offers
            .Where(o => offerIds.Contains(o.Id))
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<Offer>> GetOffersCreatedByUser(Guid userId, CancellationToken ct = default)
    {
        return await _context.Offers
            .Include(o => o.OfferDeliveries)
            .Where(o => o.CreatedByUserId == userId)
            .ToListAsync(ct);
    }
}
