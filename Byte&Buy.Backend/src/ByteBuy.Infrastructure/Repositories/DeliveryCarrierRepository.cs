using ByteBuy.Core.Domain.DeliveryCarriers;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.Filtration.DeliveryCarrier;

namespace ByteBuy.Infrastructure.Repositories;

public class DeliveryCarrierRepository : EfBaseRepository<DeliveryCarrier>, IDeliveryCarrierRepository
{
    public DeliveryCarrierRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludeId = null)
        => await _context.DeliveryCarriers
            .AnyAsync(dc => dc.Id != excludeId && (dc.Name == name || dc.Code == code));

    public async Task<IReadOnlyCollection<DeliveryCarrier>> GetAllAsync(CancellationToken ct = default)
        => await _context.DeliveryCarriers.ToListAsync(ct);

    public async Task<bool> HasActiveRelationsAsync(Guid carrierId)
        => await _context.DeliveryCarriers.AnyAsync(dc => dc.Deliveries.Any(d => d.DeliveryCarrierId == carrierId));

    public Task<PagedList<DeliveryCarrierResponse>> GetDeliveryCarrierListAsync(DeliveryCarriersListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.DeliveryCarriers
            .AsNoTracking()
            .OrderByDescending(d => d.DateCreated)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.DeliveryCarrierName))
            query = query.Where(d => EF.Functions.ILike(d.Name, $"%{queryParams.DeliveryCarrierName}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.Code))
            query = query.Where(d => EF.Functions.ILike(d.Code, $"%{queryParams.Code}%"));

        var projection = query.Select(DeliveryCarrierMappings.DeliveryCarrierResponseProjection);

        return projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }
}
