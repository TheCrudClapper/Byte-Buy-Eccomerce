using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class ConditionRepository : IConditionRepository
{
    private readonly ApplicationDbContext _context;

    public ConditionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Condition condition, CancellationToken ct)
    {
        await _context.Conditions.AddAsync(condition, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task SoftDeleteAsync(Condition condition, CancellationToken ct)
    {
        _context.Conditions.Update(condition);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Condition>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Conditions
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Condition>> GetAllByCondition(Expression<Func<Condition, bool>> expression, CancellationToken ct)
    {
        return await _context.Conditions
            .Where(expression)
            .ToListAsync(ct);
    }

    public async Task<Condition?> GetByConditionAsync(Expression<Func<Condition, bool>> expression, CancellationToken ct)
    {
        return await _context.Conditions.Where(expression)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Condition?> GetByIdAsync(Guid conditionId, CancellationToken ct)
    {
        return await _context.Conditions
            .FirstOrDefaultAsync(c => c.Id == conditionId, ct);
    }

    public async Task UpdateAsync(Condition condition, CancellationToken ct)
    {
        _context.Conditions.Update(condition);
        await _context.SaveChangesAsync(ct);
    }
}
