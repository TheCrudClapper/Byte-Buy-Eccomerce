using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly ApplicationDbContext _context;

    public DeliveryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Delivery delivery, CancellationToken ct)
    {
        await _context.Deliveries.AddAsync(delivery, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task SoftDeleteAsync(Delivery delivery, CancellationToken ct)
    {
        _context.Deliveries.Update(delivery);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Deliveries.ToListAsync(ct);
    }

    public async Task<IEnumerable<Delivery>> GetAllByCondition(Expression<Func<Delivery, bool>> expression, CancellationToken ct)
    {
        return await _context.Deliveries
            .IgnoreQueryFilters()
            .Where(expression)
            .ToListAsync(ct);
    }

    public async Task<Delivery?> GetByConditionAsync(Expression<Func<Delivery, bool>> expression, CancellationToken ct)
    {
        return await _context.Deliveries
            .IgnoreQueryFilters()
            .Where(expression)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Delivery?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Deliveries
            .FirstOrDefaultAsync(d => d.Id == id, ct);
    }

    public async Task UpdateAsync(Delivery delivery, CancellationToken ct)
    {
        _context.Deliveries.Update(delivery);
        await _context.SaveChangesAsync(ct);
    }
}
