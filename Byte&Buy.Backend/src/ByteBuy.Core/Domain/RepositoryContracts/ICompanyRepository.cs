using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICompanyRepository : IRepositoryBase<Company>
{
    Task<bool> ExistAsync(CancellationToken ct = default);
    Task<Company?> GetAsync(CancellationToken ct = default);
}
