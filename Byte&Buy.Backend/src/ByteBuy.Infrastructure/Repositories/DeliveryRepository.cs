using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
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

    public async Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Deliveries.ToListAsync(ct);
    }

    public async Task<Delivery?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Deliveries
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }
}
