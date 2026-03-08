using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyCategoryHttpClient
{
    Task<Result<PagedList<CategoryListResponse>>> GetListAsync(CategoryListQuery query);
    Task<Result<CategoryResponse>> GetByIdAsync(Guid categoryId);
    Task<Result<CreatedResponse>> PostCategoryAsync(CategoryAddRequest request);
    Task<Result<UpdatedResponse>> PutCategoryAsync(Guid categoryId, CategoryUpdateRequest request);
    Task<Result> DeleteAsync(Guid categoryId);
}
