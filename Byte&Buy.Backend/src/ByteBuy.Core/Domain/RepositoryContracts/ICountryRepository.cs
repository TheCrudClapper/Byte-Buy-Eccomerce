using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.Pagination;
using ByteBuy.Services.Filtration;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICountryRepository : IRepositoryBase<Country>
{
    Task<bool> HasActiveRelationsAsync(Guid countryId);
    Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludedId = null);
    Task<IReadOnlyCollection<Country>> GetAllAsync(CancellationToken ct = default);
    Task<PagedList<CountryResponse>> GetListAsync(CountryListQuery queryParams, CancellationToken ct = default);
}
