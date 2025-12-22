using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;
using ByteBuy.Services.DTO.Category;

namespace ByteBuy.Core.ServiceContracts;

public interface ICategoryService
    : IBaseCrudService<Guid, CategoryAddRequest, CategoryUpdateRequest, CategoryResponse>,
      ISelectableService<Guid>
{
    Task<Result<IEnumerable<CategoryListResponse>>> GetCategoriesListAsync(CancellationToken ct = default);
}
