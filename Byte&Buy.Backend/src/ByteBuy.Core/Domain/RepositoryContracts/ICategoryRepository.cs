using ByteBuy.Core.Domain.Categories;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.Filtration.Category;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> HasActiveRelations(Guid categoryId);
    Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null);
    Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken ct = default);
    Task<PagedList<CategoryListResponse>> GetCategoryListAsync(CategoryListQuery queryParams, CancellationToken ct = default);
}
