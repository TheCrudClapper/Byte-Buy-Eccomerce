using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class CountryService(ICountryHttpClient httpClient) : ICountryService
{
    public async Task<Result<CreatedResponse>> Add(CountryAddRequest request)
        => await httpClient.PostCountryAsync(request);

    public async Task<Result> DeleteById(Guid countryId)
        => await httpClient.DeleteAsync(countryId);

    public async Task<Result<IEnumerable<CountryResponse>>> GetAll()
        => await httpClient.GetCountriesAsync();

    public async Task<Result<CountryResponse>> GetById(Guid coutryId)
        => await httpClient.GetByIdAsync(coutryId);

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid countryId, CountryUpdateRequest request)
        => await httpClient.PutCountryAsync(countryId, request);
}