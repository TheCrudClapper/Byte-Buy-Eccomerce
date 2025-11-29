using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PermissionService(IPermissionHttpClient permissionHttpClient) : IPermissionService
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await permissionHttpClient.GetSelectListAsync();
}