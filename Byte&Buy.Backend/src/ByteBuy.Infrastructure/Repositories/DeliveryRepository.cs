using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
namespace ByteBuy.Infrastructure.Repositories;

public class DeliveryRepository : EfBaseRepository<Delivery>, IDeliveryRepository
{
    public DeliveryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameAsync(string name, Guid? excludedId)
    {
        return await _context.Deliveries
            .AnyAsync(d => d.Name == name && d.Id != excludedId);
    }

    public async Task<IReadOnlyCollection<Delivery>> GetAllByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        return await _context.Deliveries
            .Where(i => ids.Contains(i.Id))
            .ToListAsync(ct);
    }

    public override async Task<Delivery?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Deliveries
            .Include(d => d.DeliveryCarrier)
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task<List<Money>> GetCheapestCostByOfferIds(IEnumerable<Guid> offerIds)
    {
        return await _context.OfferDeliveries
            .AsNoTracking()
            .Where(od => offerIds.Contains(od.OfferId))
            .GroupBy(od => od.OfferId)
                .Select(g => g
                    .OrderBy(od => od.Delivery.Price.Amount)
                    .Select(od => od.Delivery.Price)
                    .First())
            .ToListAsync();
    }

    public async Task<PagedList<DeliveryListResponse>> GetDeliveriesListAsync(DeliveryListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Deliveries
            .AsNoTracking()
            .AsQueryable();

        if (queryParams.PriceFrom is not null)
            query = query.Where(d => d.Price.Amount >= queryParams.PriceFrom);

        if (queryParams.PriceTo is not null)
            query = query.Where(d => d.Price.Amount <= queryParams.PriceTo);

        if (!string.IsNullOrWhiteSpace(queryParams.DeliveryName))
            query= query.Where(d => d.Name.Contains(queryParams.DeliveryName));

        var projection = query.Select(DeliveryMappings.DeliveryListResponseProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<bool> HasActiveRelations(Guid deliveryId)
    {
        return await _context.Deliveries
            .AnyAsync(d => d.Id == deliveryId && d.OfferDeliveries.Any());
    }
}
