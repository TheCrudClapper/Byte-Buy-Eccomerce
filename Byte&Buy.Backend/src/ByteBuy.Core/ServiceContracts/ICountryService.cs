using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICountryService
{
    Task<Result<CountryResponse>> AddCountry(CountryAddRequest request, CancellationToken ct = default);
    Task<Result<CountryResponse>> UpdateCountry(Guid countryId, CountryUpdateRequest request, CancellationToken ct = default);
    Task<Result> DeleteCountry(Guid countryId, CancellationToken ct = default);
    Task<Result<CountryResponse>> GetCountry(Guid contryId, CancellationToken ct = default);
    Task<Result<IEnumerable<CountryResponse>>> GetCountries(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
}
