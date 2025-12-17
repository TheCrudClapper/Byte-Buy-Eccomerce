using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICountryRepository : IRepositoryBase<Country>
{
    Task<bool> HasActiveRelationsAsync(Guid countryId);
    Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludedId = null);
    Task<IEnumerable<Country>> GetAllAsync(CancellationToken ct = default);
    Task<Country?> GetByIdAsync(Guid countryId, CancellationToken ct = default);
}
