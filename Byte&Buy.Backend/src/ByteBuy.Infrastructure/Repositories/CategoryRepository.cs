using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class CategoryRepository : EfBaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null)
    {
        return await _context.Categories
            .AnyAsync(c => c.Name == name && c.Id != excludedId);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Categories
            .ToListAsync(ct);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

}
