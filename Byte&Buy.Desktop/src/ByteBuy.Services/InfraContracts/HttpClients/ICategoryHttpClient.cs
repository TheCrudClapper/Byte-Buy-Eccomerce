using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICategoryHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<IEnumerable<CategoryListResponse>>> GetListAsync();
    Task<Result<CategoryResponse>> GetByIdAsync(Guid categoryId);
    Task<Result<CreatedResponse>> PostCategoryAsync(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> PutCategoryAsync(Guid categoryId, CategoryUpdateRequest request);
    Task<Result> DeleteAsync(Guid categoryId);
}
