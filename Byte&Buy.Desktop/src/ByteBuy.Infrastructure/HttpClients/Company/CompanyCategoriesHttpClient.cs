using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Category;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyCategoriesHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyCategoryHttpClient
{
    private readonly string resource = options.Value.CompanyCategories;
    private readonly string optionsResource = options.Value.CategoriesOptions;

    public async Task<Result<CreatedResponse>> PostCategoryAsync(CategoryAddRequest request)
       => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCategoryAsync(Guid categoryId, CategoryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{categoryId}", request);

    public async Task<Result> DeleteAsync(Guid categoryId)
        => await DeleteAsync($"{resource}/{categoryId}");

    public async Task<Result<CategoryResponse>> GetByIdAsync(Guid categoryId)
        => await GetAsync<CategoryResponse>($"{resource}/{categoryId}");

    public async Task<Result<PagedList<CategoryListResponse>>> GetListAsync(CategoryListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<CategoryListResponse>>(url);
    }

}
