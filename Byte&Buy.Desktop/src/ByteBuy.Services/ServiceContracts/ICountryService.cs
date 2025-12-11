using ByteBuy.Core.DTO.Country;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICountryService : IBaseService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(CountryAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, CountryUpdateRequest request);
    Task<Result<CountryResponse>> GetById(Guid id);
    Task<Result<IEnumerable<CountryResponse>>> GetAll();
}