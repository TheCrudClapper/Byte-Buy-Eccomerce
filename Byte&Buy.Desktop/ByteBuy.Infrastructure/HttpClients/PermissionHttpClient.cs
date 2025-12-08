using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class PermissionHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IPermissionHttpClient
{
    private const string resource = "permissions"; 
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>($"{resource}/options");
}