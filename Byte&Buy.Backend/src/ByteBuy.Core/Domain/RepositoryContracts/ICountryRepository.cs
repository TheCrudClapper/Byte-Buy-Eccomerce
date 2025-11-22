using ByteBuy.Core.Domain.Entities;
using System.Linq.Expressions;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICountryRepository
{
    Task AddAsync(Country country);
    Task UpdateAsync(Country country);
    Task SoftDeleteAsync(Country country);
    Task<IEnumerable<Country>> GetAllAsync(CancellationToken ct = default);
    Task<Country?> GetByIdAsync(Guid countryId, CancellationToken ct = default);
    Task<IEnumerable<Country>> GetAllByCondition(Expression<Func<Country, bool>> expression, CancellationToken ct = default);
    Task<Country?> GetByConditionAsync(Expression<Func<Country, bool>> expression, CancellationToken ct = default);

}
