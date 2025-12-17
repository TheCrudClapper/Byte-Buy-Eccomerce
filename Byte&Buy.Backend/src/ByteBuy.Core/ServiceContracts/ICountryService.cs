using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface ICountryService
{
    Task<Result<CreatedResponse>> AddCountry(CountryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateCountry(Guid countryId, CountryUpdateRequest request);
    Task<Result> DeleteCountry(Guid countryId);
    Task<Result<CountryResponse>> GetCountry(Guid contryId, CancellationToken ct = default);
    Task<Result<IEnumerable<CountryResponse>>> GetCountries(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct = default);
}
