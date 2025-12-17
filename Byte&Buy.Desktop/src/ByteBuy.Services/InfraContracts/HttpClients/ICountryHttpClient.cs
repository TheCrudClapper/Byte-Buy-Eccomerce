using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICountryHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<IEnumerable<CountryResponse>>> GetCountriesAsync();
    Task<Result<CountryResponse>> GetByIdAsync(Guid countryId);
    Task<Result<CreatedResponse>> PostCountryAsync(CountryAddRequest request);
    Task<Result<UpdatedResponse>> PutCountryAsync(Guid countryId, CountryUpdateRequest request);
    Task<Result> DeleteAsync(Guid countryId);
}