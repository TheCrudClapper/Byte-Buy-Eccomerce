using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CountryHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), ICountryHttpClient
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>("countries/options");
}