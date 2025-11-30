using ByteBuy.Core.Domain.Entities;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IRoleRepository
{
    Task<IEnumerable<ApplicationRole>> GetAllAsync(CancellationToken ct = default);
    Task<ApplicationRole?> GetByIdAsync(Guid roleId, CancellationToken ct = default);
    Task<bool> ExistsAsync(string roleName, CancellationToken ct = default);
    Task<bool> DoesRoleHaveActiveUsers(Guid roleId);

}
