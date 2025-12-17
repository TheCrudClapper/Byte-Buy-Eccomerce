using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Category;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Services.DTO.Category;

namespace ByteBuy.Core.ServiceContracts;

public interface ICategoryService
{
    Task<Result<CreatedResponse>> AddCategory(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateCategory(Guid categoryId, CategoryUpdateRequest request);
    Task<Result> DeleteCategory(Guid categoryId);
    Task<Result<CategoryResponse>> GetCategory(Guid categoryId, CancellationToken ct = default);
    Task<Result<IEnumerable<CategoryListResponse>>> GetCategoriesList(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct = default);
}
