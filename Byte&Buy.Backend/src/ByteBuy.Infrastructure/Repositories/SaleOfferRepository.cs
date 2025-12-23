using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class SaleOfferRepository : EfBaseRepository<SaleOffer>, ISaleOfferRepository
{
    public SaleOfferRepository(ApplicationDbContext context) : base(context) { }

    public async Task<SaleOffer?> GetAggregateAsync(Guid id, CancellationToken ct = default)
        => await _context.SaleOffers
            .IgnoreQueryFilters()
            .Include(so => so.OfferDeliveries)
            .FirstOrDefaultAsync(so => so.Id == id, ct);
}
