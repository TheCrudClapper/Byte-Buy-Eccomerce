using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IRoleHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync();
}