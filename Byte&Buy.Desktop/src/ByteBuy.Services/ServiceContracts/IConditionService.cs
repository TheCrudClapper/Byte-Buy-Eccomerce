using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IConditionService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<CreatedResponse>> AddAsync(ConditionAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, ConditionUpdateRequest request);
    Task<Result<ConditionResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<ConditionListResponse>>> GetListAsync(ConditionListQuery query);
}
