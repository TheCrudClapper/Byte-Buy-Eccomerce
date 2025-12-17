using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IPermissionHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
}
