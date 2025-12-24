using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface ICountryService
    : IBaseCrudService<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>,
      ISelectableService<Guid>
{
    Task<Result<IReadOnlyCollection<CountryResponse>>> GetCountriesAsync(CancellationToken ct = default);
}
