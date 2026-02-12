using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.Filtration.DeliveryCarrier;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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
        => await _context.DeliveryCarriers.AnyAsync(dc => dc.Deliveries.Any());

    public Task<PagedList<DeliveryCarrierResponse>> GetDeliveryCarrierListAsync(DeliveryCarriersListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.DeliveryCarriers
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.DeliveryCarrierName))
            query = query.Where(d => d.Name.Contains(queryParams.DeliveryCarrierName));

        if (!string.IsNullOrWhiteSpace(queryParams.Code))
            query = query.Where(d => d.Code.Contains(queryParams.Code));

        var projection = query.Select(d => new DeliveryCarrierResponse(d.Id, d.Name, d.Code));

        return projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }
}
