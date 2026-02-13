using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRoleRepository : IRepositoryBase<ApplicationRole>
{
    Task<bool> ExistsByNameAsync(string roleName, CancellationToken ct = default);
    Task<bool> DoesRoleHaveActiveUsers(Guid roleId);
    Task<PagedList<RoleListResponse>> GetPagedRoleListAsync(RoleListQuery queryParams, CancellationToken ct = default);
}
