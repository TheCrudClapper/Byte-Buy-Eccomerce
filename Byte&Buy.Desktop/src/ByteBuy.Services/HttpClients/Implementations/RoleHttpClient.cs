using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class RoleHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IRoleHttpClient
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("roles/options");
}