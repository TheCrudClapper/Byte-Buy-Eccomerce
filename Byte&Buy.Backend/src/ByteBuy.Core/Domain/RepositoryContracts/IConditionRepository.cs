using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IConditionRepository : IRepositoryBase<Condition>
{
    Task<bool> HasActiveRelations(Guid conditionId);
    Task<IReadOnlyCollection<Condition>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null);
}
