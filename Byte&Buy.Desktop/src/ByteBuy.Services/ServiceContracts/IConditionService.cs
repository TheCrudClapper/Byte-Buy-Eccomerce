using ByteBuy.Services.DTO.Condition;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IConditionService : IBaseService
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(ConditionAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, ConditionUpdateRequest request);
    Task<Result<ConditionResponse>> GetById(Guid id);
    Task<Result<IEnumerable<ConditionListResponse>>> GetList();
}
