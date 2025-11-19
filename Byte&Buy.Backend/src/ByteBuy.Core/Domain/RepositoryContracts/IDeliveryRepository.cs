using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IDeliveryRepository
{
    Task AddAsync(Delivery delivery, CancellationToken ct = default);
    Task UpdateAsync(Delivery delivery, CancellationToken ct = default);
    Task SoftDeleteAsync(Delivery delivery, CancellationToken ct = default);
    Task<Delivery?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Delivery>> GetAllAsync(CancellationToken ct = default);
    /// <summary>
    /// Gets an collection of deliveries based on given expression tree, ignores global query filters
    /// </summary>
    /// <param name="expression">LINQ expression used for generating SQL Query</param>
    /// <param name="ct">Cancelation token used for cancelation of async operations</param>
    /// <returns></returns>
    Task<IEnumerable<Delivery>> GetAllByCondition(Expression<Func<Delivery, bool>> expression, CancellationToken ct = default);
    /// <summary>
    /// Gets one of deliveries based on given expression tree, ignores global query filters
    /// </summary>
    /// <param name="expression">LINQ expression used for generating SQL Query</param>
    /// <param name="ct">Cancelation token used for cancelation of async operations</param>
    /// <returns></returns>
    Task<Delivery?> GetByConditionAsync(Expression<Func<Delivery, bool>> expression, CancellationToken ct = default);
}
