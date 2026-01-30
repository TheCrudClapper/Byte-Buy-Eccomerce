namespace ByteBuy.Core.Domain.RepositoryContracts.Base;

public interface IRepositoryBase<T> : IReadRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task AddRange(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task<int> CommitAsync(CancellationToken ct = default);
}
