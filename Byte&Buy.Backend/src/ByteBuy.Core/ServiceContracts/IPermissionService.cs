using ByteBuy.Core.DTO;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct = default);
}
