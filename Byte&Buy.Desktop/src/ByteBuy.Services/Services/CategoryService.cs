using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
namespace ByteBuy.Services.Services;

public class CategoryService(ICompanyCategoryHttpClient httpClient, IPublicCategoriesHttpClient publicClient) 
    : ICategoryService
{
    public async Task<Result<CreatedResponse>> AddAsync(CategoryAddRequest request)
        => await httpClient.PostCategoryAsync(request);

    public Task<Result> DeleteByIdAsync(Guid id)
        => httpClient.DeleteAsync(id);

    public async Task<Result<CategoryResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<CategoryListResponse>>> GetListAsync(CategoryListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await publicClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, CategoryUpdateRequest request)
        => await httpClient.PutCategoryAsync(id, request);
}
