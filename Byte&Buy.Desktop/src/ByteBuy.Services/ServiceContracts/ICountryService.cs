using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICountryService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
    Task<Result<CreatedResponse>> AddCountry(CountryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateCountry(Guid countryId, CountryUpdateRequest request);
    Task<Result> DeleteCountry(Guid countryId);
    Task<Result<CountryResponse>> GetCountry(Guid coutryId);
    Task<Result<IEnumerable<CountryResponse>>> GetCountries();
}