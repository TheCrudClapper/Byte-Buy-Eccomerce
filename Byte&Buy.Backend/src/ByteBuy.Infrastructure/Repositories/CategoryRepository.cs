using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.Filtration.Category;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
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

    public async Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Categories
            .ToListAsync(ct);
    }

    public async Task<PagedList<CategoryListResponse>> GetCategoryListAsync(CategoryListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Categories
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.CategoryName))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{queryParams.CategoryName}%"));

        var projection = query.Select(CategoryMappings.CategoryListProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task<bool> HasActiveRelations(Guid categoryId)
    {
        return await _context.Conditions
            .AnyAsync(c => c.Id == categoryId && c.Products.Any());
    }
}
