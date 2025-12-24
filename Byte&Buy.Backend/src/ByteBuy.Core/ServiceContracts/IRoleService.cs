using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IRoleService
    : IBaseCrudService<Guid, RoleAddRequest, RoleUpdateRequest, RoleResponse>,
      ISelectableService<Guid>
{
    public Task<Result<IReadOnlyCollection<RoleResponse>>> GetAllRolesAsync(CancellationToken ct = default);
}
