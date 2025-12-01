using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class RoleHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IRoleHttpClient
{
    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RoleUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"roles/{id}", request);

    public async Task<Result<CreatedResponse>> AddAsync(RoleAddRequest request)
        => await PostAsync<CreatedResponse>("roles", request);

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("roles/options");

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllAsync()
        => await GetAsync<IEnumerable<RoleResponse>>("roles");

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await DeleteAsync($"roles/{id}");

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid id)
        => await GetAsync<RoleResponse>($"roles/{id}");
}