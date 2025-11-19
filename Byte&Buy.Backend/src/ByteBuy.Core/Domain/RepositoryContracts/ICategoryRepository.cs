using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICategoryRepository
{
    Task AddAsync(Category category, CancellationToken ct = default);
    Task UpdateAsync(Category category, CancellationToken ct = default);
    Task SoftDeleteAsync(Category category, CancellationToken ct = default);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Category>> GetAllByCondition(Expression<Func<Category, bool>> expression, CancellationToken ct = default);
    Task<Category?> GetByConditionAsync(Expression<Func<Category, bool>> expression, CancellationToken ct = default);
}
