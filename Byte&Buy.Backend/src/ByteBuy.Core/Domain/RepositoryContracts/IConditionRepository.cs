using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IConditionRepository
{
    Task AddAsync(Condition condition, CancellationToken ct = default);
    Task UpdateAsync(Condition condition, CancellationToken ct = default);
    Task SoftDeleteAsync(Condition condition, CancellationToken ct = default);
    Task<Condition?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Condition>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Condition>> GetAllByCondition(Expression<Func<Condition, bool>> expression, CancellationToken ct = default);
    Task<Condition?> GetByConditionAsync(Expression<Func<Condition, bool>> expression, CancellationToken ct = default);
}
