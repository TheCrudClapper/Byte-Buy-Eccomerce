using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Category category, CancellationToken ct)
    {
        await _context.Categories.AddAsync(category, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task SoftDeleteAsync(Category category, CancellationToken ct)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Categories.ToListAsync(ct);
    }

    public async Task<IEnumerable<Category>> GetAllByCondition(Expression<Func<Category, bool>> expression, CancellationToken ct)
    {
        return await _context.Categories
            .IgnoreQueryFilters()
            .Where(expression)
            .ToListAsync(ct);
    }

    public async Task<Category?> GetByConditionAsync(Expression<Func<Category, bool>> expression, CancellationToken ct)
    {
        return await _context.Categories
            .IgnoreQueryFilters()
            .Where(expression)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task UpdateAsync(Category category, CancellationToken ct)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync(ct);
    }
}
