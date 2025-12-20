using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> HasActiveRelations(Guid categoryId);
    Task<bool> ExistWithNameAsync(string name, Guid? excludedId = null);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct = default);
}
