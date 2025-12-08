using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CountryHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), ICountryHttpClient
{
    private const string resource = "countries";

    public async Task<Result<CreatedResponse>> PostCountryAsync(CountryAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCountryAsync(Guid countryId, CountryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{countryId}", request);
    
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>($"{resource}/options");

    public async Task<Result<IEnumerable<CountryResponse>>> GetCountriesAsync()
        => await GetAsync<IEnumerable<CountryResponse>>($"{resource}");

    public async Task<Result<CountryResponse>> GetByIdAsync(Guid countryId)
        => await GetAsync<CountryResponse>($"{resource}/{countryId}");

    public async Task<Result> DeleteAsync(Guid countryId)
       => await DeleteAsync($"{resource}/{countryId}");

}