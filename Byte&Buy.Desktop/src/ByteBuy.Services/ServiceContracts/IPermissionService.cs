using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPermissionService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
}
