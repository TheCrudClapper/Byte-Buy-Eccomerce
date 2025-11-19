using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICountryRepository
{
    Task AddAsync(Country country, CancellationToken ct = default);
    Task UpdateAsync(Country country, CancellationToken ct = default);
    Task SoftDeleteAsync(Country country, CancellationToken ct = default);
    Task<IEnumerable<Country>> GetAllAsync(CancellationToken ct = default);
    Task<Country?> GetByIdAsync(Guid countryId, CancellationToken ct = default);
    Task<IEnumerable<Country>> GetAllByCondition(Expression<Func<Country, bool>> expression, CancellationToken ct = default);
    Task<Country?> GetByConditionAsync(Expression<Func<Country, bool>> expression, CancellationToken ct = default);

}
