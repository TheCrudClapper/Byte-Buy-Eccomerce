using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RoleRepository : EfBaseRepository<ApplicationRole>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> DoesRoleHaveActiveUsers(Guid roleId)
    {
        return await _context.UserRoles
            .AnyAsync(ur => ur.RoleId == roleId && ur.User.IsActive);
    }

    public async Task<bool> ExistsBynameAsync(string roleName, CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name == roleName, ct);
    }
}
