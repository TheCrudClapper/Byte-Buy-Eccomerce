using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;
using ByteBuy.Services.Filtration;

namespace ByteBuy.Core.ServiceContracts;

public interface ICountryService
    : IBaseCrudService<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<CountryResponse>>> GetCountriesListAsync(CountryListQuery queryParams, CancellationToken ct = default);
}
