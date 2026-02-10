using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PermissionService(IPermissionHttpClient permissionHttpClient) : IPermissionService
{
    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList()
        => await permissionHttpClient.GetSelectListAsync();
}