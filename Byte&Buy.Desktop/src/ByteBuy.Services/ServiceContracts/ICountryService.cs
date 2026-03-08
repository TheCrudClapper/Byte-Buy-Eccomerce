using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICountryService : IBaseService
{
    Task<Result<CreatedResponse>> AddAsync(CountryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, CountryUpdateRequest request);
    Task<Result<CountryResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<CountryResponse>>> GetListAsync(CountryListQuery query);
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
}