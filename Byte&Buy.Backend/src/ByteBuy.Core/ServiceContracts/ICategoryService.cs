using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.Filtration.Category;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface ICategoryService
    : IBaseCrudService<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<CategoryListResponse>>> GetCategoriesListAsync(CategoryListQuery queryParams, CancellationToken ct = default);
}
