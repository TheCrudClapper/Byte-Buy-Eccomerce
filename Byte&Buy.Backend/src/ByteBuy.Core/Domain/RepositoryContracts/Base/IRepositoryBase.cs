namespace ByteBuy.Core.Domain.RepositoryContracts.Base;

public interface IRepositoryBase<T> : IReadRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}
