using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICategoryService
{
    Task<Result<CategoryResponse>> AddCategory(CategoryAddRequest request, CancellationToken ct = default);
    Task<Result<CategoryResponse>> UpdateCategory(Guid categoryId, CategoryUpdateRequest request, CancellationToken ct = default);
    Task<Result> DeleteCategory(Guid categoryId, CancellationToken ct = default);
    Task<Result<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken ct = default);
    Task<Result<IEnumerable<CategoryResponse>>> GetCategories(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
}
