using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class ConditionService(ICompanyConditionHttpClient httpClient, IPublicConditionsHttpClients publicClient)
    : IConditionService
{
    public async Task<Result<CreatedResponse>> AddAsync(ConditionAddRequest request)
        => await httpClient.PostConditionAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<ConditionResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<PagedList<ConditionListResponse>>> GetListAsync(ConditionListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await publicClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ConditionUpdateRequest request)
        => await httpClient.PutConditionAsync(id, request);
}
