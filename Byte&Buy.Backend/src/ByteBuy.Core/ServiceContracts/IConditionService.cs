using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IConditionService
    : IBaseCrudService<Guid, ConditionAddRequest, ConditionUpdateRequest, ConditionResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<ConditionListResponse>>> GetConditionsListAsync(PaginationParameters parameters, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ConditionResponse>>> GetConditionsAsync(CancellationToken ct = default);
}
