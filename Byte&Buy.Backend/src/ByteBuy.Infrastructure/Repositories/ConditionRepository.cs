using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class ConditionRepository : EfBaseRepository<Condition>, IConditionRepository
{
    public ConditionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null)
    {
        return await _context.Conditions
            .AnyAsync(c => c.Name == name && c.Id != excludedId);
    }

    public async Task<IEnumerable<Condition>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Conditions
            .ToListAsync(ct);
    }

    public async Task<Condition?> GetByIdAsync(Guid conditionId, CancellationToken ct)
    {
        return await _context.Conditions
            .FirstOrDefaultAsync(c => c.Id == conditionId, ct);
    }
}
