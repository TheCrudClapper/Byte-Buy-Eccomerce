using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class CountryService(ICountryHttpClient httpClient) : ICountryService
{
    public async Task<Result<CreatedResponse>> AddCountry(CountryAddRequest request)
        => await httpClient.PostCountryAsync(request);

    public async Task<Result> DeleteCountry(Guid countryId)
        => await httpClient.DeleteAsync(countryId);

    public async Task<Result<IEnumerable<CountryResponse>>> GetCountries()
        => await httpClient.GetCountriesAsync();

    public async Task<Result<CountryResponse>> GetCountry(Guid coutryId)
        => await httpClient.GetByIdAsync(coutryId);
      
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateCountry(Guid countryId, CountryUpdateRequest request)
        => await httpClient.PutCountryAsync(countryId, request);
}