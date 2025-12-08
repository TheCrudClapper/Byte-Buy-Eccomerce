using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CategoryHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), ICategoryHttpClient
{

    private const string resource = "categories";

    public async Task<Result<CreatedResponse>> PostCategoryAsync(CategoryAddRequest request)
       => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCategoryAsync(Guid categoryId, CategoryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{categoryId}", request);

    public async Task<Result> DeleteAsync(Guid categoryId)
        => await DeleteAsync($"{resource}/{categoryId}");

    public async Task<Result<CategoryResponse>> GetByIdAsync(Guid categoryId)
        => await GetAsync<CategoryResponse>($"{resource}/{categoryId}");

    public async Task<Result<IEnumerable<CategoryListResponse>>> GetListAsync()
        => await GetAsync<IEnumerable<CategoryListResponse>>($"{resource}/list");

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>($"{resource}/options");


}
