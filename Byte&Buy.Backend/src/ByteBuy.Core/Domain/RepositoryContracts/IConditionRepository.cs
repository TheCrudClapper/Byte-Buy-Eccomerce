using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Condition;
using ByteBuy.Core.Filtration.Condition;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IConditionRepository : IRepositoryBase<Condition>
{
    Task<bool> HasActiveRelations(Guid conditionId);
    Task<PagedList<ConditionListResponse>> GetPagedListAsync(ConditionListQuery queryParams, CancellationToken ct = default);
    Task<IReadOnlyCollection<Condition>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null);
}
