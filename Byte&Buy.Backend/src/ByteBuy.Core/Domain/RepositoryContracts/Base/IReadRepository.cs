using Ardalis.Specification;
using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts.Base;

public interface IReadRepository<T> where T : class
{
    //Used for fetching entites
    Task<T?> GetBySpecAsync(ISpecification<T> spec, CancellationToken ct = default);
    //Used for fetching dtos
    Task<TResult?> GetBySpecAsync<TResult>(
      ISpecification<T, TResult> spec, CancellationToken ct = default);

    //Used for fetching collection of entities
    Task<List<T>> GetListBySpecAsync(ISpecification<T> spec, CancellationToken ct = default);

    //Used for fetching collection od dtos
    Task<List<TResult>> GetListBySpecAsync<TResult>(ISpecification<T, TResult> spec, CancellationToken ct = default);

    //Gets list of enitites by given expression
    Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default);

    //Gets entity by given expression
    Task<T?> GetByConditionAsync(Expression<Func<T, bool>> expression, CancellationToken ct = default);
}
