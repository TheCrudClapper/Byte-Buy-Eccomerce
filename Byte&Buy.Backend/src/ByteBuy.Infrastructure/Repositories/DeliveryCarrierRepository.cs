using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class DeliveryCarrierRepository : EfBaseRepository<DeliveryCarrier>, IDeliveryCarrierRepository
{
    public DeliveryCarrierRepository(ApplicationDbContext context) : base(context) { }
    public async Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludeId = null)
        => await _context.DeliveryCarriers
            .AnyAsync(dc => dc.Id != excludeId && (dc.Name == name || dc.Code == code));
    public async Task<IEnumerable<DeliveryCarrier>> GetAllAsync(CancellationToken ct = default)
        => await _context.DeliveryCarriers.ToListAsync(ct);

    public async Task<bool> HasActiveRelationsAsync(Guid carrierId)
        => await _context.DeliveryCarriers.AnyAsync(dc => dc.Deliveries.Any());
}
