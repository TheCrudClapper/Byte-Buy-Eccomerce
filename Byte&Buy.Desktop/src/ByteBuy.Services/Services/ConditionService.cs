using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class ConditionService(IConditionHttpClient httpClient) : IConditionService
{
    public async Task<Result<CreatedResponse>> Add(ConditionAddRequest request)
        => await httpClient.PostConditionAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<ConditionResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IEnumerable<ConditionListResponse>>> GetList()
        => await httpClient.GetListAsync();

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid id, ConditionUpdateRequest request)
        => await httpClient.PutConditionAsync(id, request);
}
