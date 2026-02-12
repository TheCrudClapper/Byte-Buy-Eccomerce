using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICountryHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<PagedList<CountryResponse>>> GetCountryListAsync(CountryListQuery query);
    Task<Result<CountryResponse>> GetByIdAsync(Guid countryId);
    Task<Result<CreatedResponse>> PostCountryAsync(CountryAddRequest request);
    Task<Result<UpdatedResponse>> PutCountryAsync(Guid countryId, CountryUpdateRequest request);
    Task<Result> DeleteAsync(Guid countryId);
}