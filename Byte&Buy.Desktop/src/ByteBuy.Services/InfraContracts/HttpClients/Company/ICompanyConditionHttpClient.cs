using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyConditionHttpClient
{
    Task<Result<PagedList<ConditionListResponse>>> GetListAsync(ConditionListQuery query);
    Task<Result<ConditionResponse>> GetByIdAsync(Guid categoryId);
    Task<Result<CreatedResponse>> PostConditionAsync(ConditionAddRequest request);
    Task<Result<UpdatedResponse>> PutConditionAsync(Guid conditionId, ConditionUpdateRequest request);
    Task<Result> DeleteAsync(Guid conditionId);
}
