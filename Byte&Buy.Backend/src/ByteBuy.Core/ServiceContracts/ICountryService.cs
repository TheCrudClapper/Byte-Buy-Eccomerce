using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface ICountryService
    : IBaseCrudService<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<CountryResponse>>> GetCountriesListAsync(CountryListQuery queryParams, CancellationToken ct = default);
}
