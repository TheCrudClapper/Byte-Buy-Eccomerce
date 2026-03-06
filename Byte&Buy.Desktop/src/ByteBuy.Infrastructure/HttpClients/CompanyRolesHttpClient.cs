using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyRolesHttpClient(HttpClient httpClient, IConfiguration config) 
    : HttpClientBase(httpClient, config), IRoleHttpClient
{
    private const string resource = "company/roles";
    public async Task<Result<UpdatedResponse>> PutAsync(Guid roleId, RoleUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{roleId}", request);

    public async Task<Result<CreatedResponse>> PostAsync(RoleAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListItemsAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"{resource}/options");

    public Task<Result<PagedList<RoleListResponse>>> GetListAsync(RoleListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return GetAsync<PagedList<RoleListResponse>>(url);
    }

    public async Task<Result> DeleteByIdAsync(Guid roleId)
        => await DeleteAsync($"{resource}/{roleId}");

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid roleId)
        => await GetAsync<RoleResponse>($"{resource}/{roleId}");
}