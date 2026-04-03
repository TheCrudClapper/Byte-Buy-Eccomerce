using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyPermissionHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyPermissionHttpClient
{
    private readonly string resource = options.Value.CompanyPermissions;

    public async Task<Result> DeleteAsync(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<PermissionResponse>> GetByIdAsync(Guid id)
        => await GetAsync<PermissionResponse>($"{resource}/{id}");

    public async Task<Result<IReadOnlyCollection<PermissionResponse>>> GetListAsync()
        => await GetAsync<IReadOnlyCollection<PermissionResponse>>($"{resource}");

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{resource}/options");

    public async Task<Result<CreatedResponse>> PostAsync(PermissionAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutAsync(Guid id, PermissionUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}