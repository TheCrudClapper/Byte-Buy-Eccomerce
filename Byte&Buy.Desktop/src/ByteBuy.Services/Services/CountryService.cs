using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
using System.Collections.ObjectModel;

namespace ByteBuy.Services.Services;

public class CountryService(ICompanyCountryHttpClient httpClient, IPublicCountriesHttpClient publicClient) 
    : ICountryService
{
    public async Task<Result<CreatedResponse>> AddAsync(CountryAddRequest request)
        => await httpClient.PostCountryAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid countryId)
        => await httpClient.DeleteAsync(countryId);

    public async Task<Result<CountryResponse>> GetByIdAsync(Guid coutryId)
        => await httpClient.GetByIdAsync(coutryId);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await publicClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid countryId, CountryUpdateRequest request)
        => await httpClient.PutCountryAsync(countryId, request);

    public async Task<Result<PagedList<CountryResponse>>> GetListAsync(CountryListQuery query)
        => await httpClient.GetCountryListAsync(query);
}