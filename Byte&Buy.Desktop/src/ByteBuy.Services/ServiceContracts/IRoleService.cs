using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRoleService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetRolesAsSelectList();
}
