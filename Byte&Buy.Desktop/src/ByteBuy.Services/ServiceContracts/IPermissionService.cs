using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPermissionService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<CreatedResponse>> AddAsync(PermissionAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PermissionUpdateRequest request);
    Task<Result<PermissionResponse>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyCollection<PermissionResponse>>> GetListAsync();
}
