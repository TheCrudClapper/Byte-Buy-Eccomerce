using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ICountryService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
}