using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ICountryHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync();
}