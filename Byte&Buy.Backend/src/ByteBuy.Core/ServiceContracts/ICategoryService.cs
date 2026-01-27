using ByteBuy.Core.DTO.Public.Category;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface ICategoryService
    : IBaseCrudService<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>,
      ISelectableService<Guid>
{
    Task<Result<IReadOnlyCollection<CategoryListResponse>>> GetCategoriesListAsync(CancellationToken ct = default);
}
