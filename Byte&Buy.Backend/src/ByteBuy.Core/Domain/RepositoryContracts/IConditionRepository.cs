using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IConditionRepository : IRepositoryBase<Condition>
{
    Task<bool> HasActiveRelations(Guid conditionId);
    Task<PagedList<Condition>> GetPagedListAsync(PaginationParameters parameters, CancellationToken ct = default);
    Task<IReadOnlyCollection<Condition>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null);
}
