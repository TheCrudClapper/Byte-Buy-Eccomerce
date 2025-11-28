using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ModelsUI.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class RoleHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IRoleHttpClient
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("roles/options");

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync()
        => await GetAsync<IEnumerable<RoleResponse>>("roles");
    
}