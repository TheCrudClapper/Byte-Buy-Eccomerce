using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPermissionService
    : IBaseCrudService<Guid, PermissionAddRequest, PermissionUpdateRequest, PermissionResponse>, ISelectableService<Guid>
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    Task<Result<IReadOnlyCollection<PermissionResponse>>> GetPermissionListAsync(CancellationToken ct = default);
}
