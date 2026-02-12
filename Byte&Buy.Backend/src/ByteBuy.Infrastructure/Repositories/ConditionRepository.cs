using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class ConditionRepository : EfBaseRepository<Condition>, IConditionRepository
{
    public ConditionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null)
    {
        return await _context.Conditions
            .AnyAsync(c => c.Name == name && c.Id != excludedId);
    }

    public async Task<IReadOnlyCollection<Condition>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Conditions
            .ToListAsync(ct);
    }

    public async Task<PagedList<Condition>> GetPagedListAsync(PaginationParameters parameters, CancellationToken ct = default)
    {
        return await _context.Conditions
            .ToPagedListAsync(parameters);
    }

    public async Task<bool> HasActiveRelations(Guid conditionId)
    {
        return await _context.Conditions
            .AnyAsync(c => c.Id == conditionId && c.Products.Any());
    }
}
