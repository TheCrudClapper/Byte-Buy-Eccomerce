using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.Filtration.Condition;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IConditionService
    : IBaseCrudService<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<ConditionListResponse>>> GetConditionsListAsync(ConditionListQuery queryParams, CancellationToken ct = default);
}
