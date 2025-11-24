using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IPermissionHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync();
}
