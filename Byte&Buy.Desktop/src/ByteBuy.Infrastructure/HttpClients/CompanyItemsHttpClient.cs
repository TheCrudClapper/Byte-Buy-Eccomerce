using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyItemsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IItemHttpClient
{
    private readonly string resource = options.Value.CompanyItems;

    public async Task<Result> DeleteCompanyItem(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<ItemResponse>> GetByIdAsync(Guid id)
        => await GetAsync<ItemResponse>($"{resource}/{id}");

    public async Task<Result<PagedList<ItemListResponse>>> GetListAsync(ItemListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<ItemListResponse>>(url);
    }

    public async Task<Result<CreatedResponse>> PostCompanyItem(MultipartContent request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCompanyItem(Guid id, MultipartContent request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
