using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts.Base;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRoleRepository : IRepositoryBase<ApplicationRole>
{
    Task<bool> ExistsBynameAsync(string roleName, CancellationToken ct = default);
    Task<bool> DoesRoleHaveActiveUsers(Guid roleId);
}
