using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class PermissionHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IPermissionHttpClient
{
    private const string resource = "permissions";
    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{resource}/options");
}