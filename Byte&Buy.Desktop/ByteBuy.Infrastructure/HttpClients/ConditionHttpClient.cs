using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class ConditionHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IConditionHttpClient
{
    private const string resource = "conditions";

    public async Task<Result<CreatedResponse>> PostConditionAsync(ConditionAddRequest request)
       => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutConditionAsync(Guid conditionId, ConditionUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{conditionId}", request);

    public async Task<Result> DeleteAsync(Guid conditionId)
       => await DeleteAsync($"{resource}/{conditionId}");

    public async Task<Result<ConditionResponse>> GetByIdAsync(Guid categoryId)
        => await GetAsync<ConditionResponse>($"{resource}/{categoryId}");

    public async Task<Result<IEnumerable<ConditionListResponse>>> GetConditionList()
        => await GetAsync<IEnumerable<ConditionListResponse>>($"{resource}/list");

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>($"{resource}/options");
}
