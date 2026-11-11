using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.Filtration.Permission;
using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyPermissionHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory), ICompanyPermissionHttpClient
{
    private readonly string resource = options.Value.CompanyPermissions;

    public async Task<Result> DeleteAsync(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<PermissionResponse>> GetByIdAsync(Guid id)
        => await GetAsync<PermissionResponse>($"{resource}/{id}");

    public async Task<Result<PagedList<PermissionResponse>>> GetListAsync(PermissionListQuery queryParams, CancellationToken cancellationToken = default)
    {
        var url = QueryStringHelper.ToQueryString(resource, queryParams);
        return await GetAsync<PagedList<PermissionResponse>>(url, cancellationToken);
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{resource}/options");

    public async Task<Result<CreatedResponse>> PostAsync(PermissionAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutAsync(Guid id, PermissionUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}