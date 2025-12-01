using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class RoleHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IRoleHttpClient
{
    public async Task<Result<RoleResponse>> UpdateAsync(Guid id, RoleUpdateRequest request)
        => await PutAsync<RoleResponse>($"roles/{id}", request);

    public async Task<Result<RoleResponse>> AddAsync(RoleAddRequest request)
        => await PostAsync<RoleResponse>("roles",request);

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("roles/options");

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync()
        => await GetAsync<IEnumerable<RoleResponse>>("roles");

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await DeleteAsync($"roles/{id}");

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid id)
        => await GetAsync<RoleResponse>($"roles/{id}");
}