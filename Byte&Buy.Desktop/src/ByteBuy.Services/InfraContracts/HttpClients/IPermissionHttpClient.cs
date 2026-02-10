using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IPermissionHttpClient
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
}
