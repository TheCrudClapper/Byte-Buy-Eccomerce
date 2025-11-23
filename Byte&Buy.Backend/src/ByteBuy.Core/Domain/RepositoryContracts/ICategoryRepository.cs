using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICategoryRepository
{
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Category>> GetAllByCondition(Expression<Func<Category, bool>> expression, CancellationToken ct = default);
    Task<Category?> GetByConditionAsync(Expression<Func<Category, bool>> expression, CancellationToken ct = default);
}
