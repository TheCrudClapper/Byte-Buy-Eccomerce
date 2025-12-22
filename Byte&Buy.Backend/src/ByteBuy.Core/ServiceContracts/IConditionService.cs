using ByteBuy.Core.DTO.Condition;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IConditionService
    : IBaseCrudService<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>,
      ISelectableService<Guid>
{
    Task<Result<IEnumerable<ConditionListResponse>>> GetConditionsListAsync(CancellationToken ct = default);
    Task<Result<IEnumerable<ConditionResponse>>> GetConditionsAsync(CancellationToken ct = default);
}
