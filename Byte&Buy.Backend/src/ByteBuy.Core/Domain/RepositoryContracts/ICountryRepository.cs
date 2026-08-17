using ByteBuy.Core.Domain.Countries;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Country;


namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface ICountryRepository : IRepositoryBase<Country>
{
    Task<bool> HasActiveRelationsAsync(Guid countryId);
    Task<bool> ExistWithNameOrCodeAsync(string name, string code, Guid? excludedId = null);
    Task<IReadOnlyCollection<Country>> GetAllAsync(CancellationToken ct = default);
    Task<PagedList<CountryResponse>> GetListAsync(CountryListQuery queryParams, CancellationToken ct = default);
}
