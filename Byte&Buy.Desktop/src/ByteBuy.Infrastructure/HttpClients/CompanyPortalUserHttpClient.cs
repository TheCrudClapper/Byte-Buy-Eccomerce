using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyPortalUserHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IPortalUserHttpClient
{
    private const string resource = "company/portal-users";

    public async Task<Result<CreatedResponse>> PostPortalUserAsync(PortalUserAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);
    public async Task<Result<UpdatedResponse>> PutPortalUserAsync(Guid userId, PortalUserUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{userId}", request);

    public async Task<Result> DeleteByIdAsync(Guid userId)
        => await DeleteAsync($"{resource}/{userId}");

    public async Task<Result<PortalUserResponse>> GetByIdAsync(Guid userId)
        => await GetAsync<PortalUserResponse>($"{resource}/{userId}");

    public Task<Result<PagedList<PortalUserListResponse>>> GetListAsync(PortalUserListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return GetAsync<PagedList<PortalUserListResponse>>(url);
    }
}