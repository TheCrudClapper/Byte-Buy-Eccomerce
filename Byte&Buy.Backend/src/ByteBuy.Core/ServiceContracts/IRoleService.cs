using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IRoleService
    : IBaseCrudService<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>,
      ISelectableService<Guid>
{
    public Task<Result<PagedList<RoleListResponse>>> GetRolesListAsync(RoleListQuery queryParams, CancellationToken ct = default);
}
