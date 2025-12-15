using ByteBuy.Core.DTO.Item;
using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class ItemHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IItemHttpClient
{
    private const string resource = "items";

    public async Task<Result> DeleteCompanyItem(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<ItemResponse>> GetByIdAsync(Guid id)
        => await GetAsync<ItemResponse>($"{resource}/{id}");

    public async Task<Result<IReadOnlyCollection<ItemListResponse>>> GetListAsync()
        => await GetAsync<IReadOnlyCollection<ItemListResponse>>($"{resource}/list");

    public async Task<Result<CreatedResponse>> PostCompanyItem(MultipartContent request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCompanyItem(Guid id, ItemUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
