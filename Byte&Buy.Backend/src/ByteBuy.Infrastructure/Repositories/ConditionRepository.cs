using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.Filtration.Condition;
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

    public async Task<PagedList<ConditionListResponse>> GetPagedListAsync(ConditionListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Conditions
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.ConditionName))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{queryParams.ConditionName}%"));

        var projection = query.Select(c => new ConditionListResponse(c.Id, c.Name));

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<bool> HasActiveRelations(Guid conditionId)
    {
        return await _context.Conditions
            .AnyAsync(c => c.Id == conditionId && c.Products.Any());
    }
}
