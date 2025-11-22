using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IConditionRepository
{
    Task AddAsync(Condition condition);
    Task UpdateAsync(Condition condition);
    Task SoftDeleteAsync(Condition condition);
    Task<Condition?> GetByIdAsync(Guid conditionId, CancellationToken ct = default);
    Task<IEnumerable<Condition>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Condition>> GetAllByCondition(Expression<Func<Condition, bool>> expression, CancellationToken ct = default);
    Task<Condition?> GetByConditionAsync(Expression<Func<Condition, bool>> expression, CancellationToken ct = default);
}
