using ByteBuy.Core.DTO.Country;
using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyCountryHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyCountryHttpClient
{
    private readonly string resource = options.Value.CompanyCountries;

    public async Task<Result<CreatedResponse>> PostCountryAsync(CountryAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutCountryAsync(Guid countryId, CountryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{countryId}", request);

    public async Task<Result<CountryResponse>> GetByIdAsync(Guid countryId)
        => await GetAsync<CountryResponse>($"{resource}/{countryId}");

    public async Task<Result> DeleteAsync(Guid countryId)
       => await DeleteAsync($"{resource}/{countryId}");

    public async Task<Result<PagedList<CountryResponse>>> GetCountryListAsync(CountryListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<CountryResponse>>(url);
    }
}