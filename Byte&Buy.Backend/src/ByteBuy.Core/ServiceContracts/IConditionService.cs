using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IConditionService
{
    Task<Result<ConditionResponse>> AddCondition(ConditionAddRequest request);
    Task<Result<ConditionResponse>> UpdateCondition(Guid conditionId, ConditionUpdateRequest request);
    Task<Result> DeleteCondition(Guid conditionId);
    Task<Result<ConditionResponse>> GetCondition(Guid conditionId, CancellationToken ct = default);
    Task<Result<IEnumerable<ConditionResponse>>> GetConditions(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
}
