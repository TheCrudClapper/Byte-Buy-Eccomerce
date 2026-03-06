using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class ConditionHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IConditionHttpClient
{
    private const string resource = "company/conditions";

    public async Task<Result<CreatedResponse>> PostConditionAsync(ConditionAddRequest request)
       => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutConditionAsync(Guid conditionId, ConditionUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{conditionId}", request);

    public async Task<Result> DeleteAsync(Guid conditionId)
       => await DeleteAsync($"{resource}/{conditionId}");

    public async Task<Result<ConditionResponse>> GetByIdAsync(Guid categoryId)
        => await GetAsync<ConditionResponse>($"{resource}/{categoryId}");

    public async Task<Result<PagedList<ConditionListResponse>>> GetListAsync(ConditionListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<ConditionListResponse>>(url);
    }

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"conditions/options");
}
