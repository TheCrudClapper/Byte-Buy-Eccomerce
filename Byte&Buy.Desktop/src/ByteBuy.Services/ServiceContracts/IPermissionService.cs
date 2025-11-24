using ByteBuy.Services.DTO;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPermissionService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
}
