using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.Filtration.Permission;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPermissionService
    : IBaseCrudService<Guid, PermissionAddRequest, PermissionUpdateRequest, PermissionResponse>, ISelectableService<Guid>
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    Task<Result<PagedList<PermissionResponse>>> GetPermissionListAsync(PermissionListQuery queryParams, CancellationToken ct = default);
}
