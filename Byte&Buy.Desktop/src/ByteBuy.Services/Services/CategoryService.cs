using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
namespace ByteBuy.Services.Services;

public class CategoryService(ICategoryHttpClient httpClient) : ICategoryService
{
    public async Task<Result<CreatedResponse>> Add(CategoryAddRequest request)
        => await httpClient.PostCategoryAsync(request);

    public Task<Result> DeleteById(Guid id)
        => httpClient.DeleteAsync(id);

    public async Task<Result<CategoryResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IEnumerable<CategoryListResponse>>> GetList()
        => await httpClient.GetListAsync();

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid id, CategoryUpdateRequest request)
        => await httpClient.PutCategoryAsync(id , request);
}
