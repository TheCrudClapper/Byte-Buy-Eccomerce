using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyRolesHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory), ICompanyRoleHttpClient
{
    private readonly string resource = options.Value.CompanyRoles;
    public async Task<Result<UpdatedResponse>> PutAsync(Guid roleId, RoleUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{roleId}", request);

    public async Task<Result<CreatedResponse>> PostAsync(RoleAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{resource}/options");

    public Task<Result<PagedList<RoleListResponse>>> GetListAsync(RoleListQuery query, CancellationToken cancellationToken = default)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return GetAsync<PagedList<RoleListResponse>>(url, cancellationToken);
    }

    public async Task<Result> DeleteByIdAsync(Guid roleId)
        => await DeleteAsync($"{resource}/{roleId}");

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid roleId)
        => await GetAsync<RoleResponse>($"{resource}/{roleId}");
}