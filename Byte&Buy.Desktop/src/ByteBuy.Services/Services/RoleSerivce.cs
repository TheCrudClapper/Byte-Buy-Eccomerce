using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RoleService(IRoleHttpClient roleClient) : IRoleService
{
    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetRolesAsSelectList()
    {
        var rolesResult = await roleClient.GetSelectListItemsAsync();
        return rolesResult.Map();
    }
}
