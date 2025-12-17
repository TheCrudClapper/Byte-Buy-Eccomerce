using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class RoleHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IRoleHttpClient
{
    private const string resource = "roles";
    public async Task<Result<UpdatedResponse>> PutAsync(Guid roleId, RoleUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{roleId}", request);

    public async Task<Result<CreatedResponse>> PostAsync(RoleAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"{resource}/options");

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync()
        => await GetAsync<IEnumerable<RoleResponse>>($"{resource}");

    public async Task<Result> DeleteByIdAsync(Guid roleId)
        => await DeleteAsync($"{resource}/{roleId}");

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid roleId)
        => await GetAsync<RoleResponse>($"{resource}/{roleId}");
}