using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> DoesRoleHaveActiveUsers(Guid roleId)
    {
        return await _context.UserRoles
            .AnyAsync(ur => ur.RoleId == roleId && ur.User.IsActive);
    }

    public async Task<bool> ExistsAsync(string roleName, CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name == roleName, ct);
    }

    public async Task<IEnumerable<ApplicationRole>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<ApplicationRole?> GetByIdAsync(Guid roleId, CancellationToken ct)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == roleId, ct);
    }

    public async Task<IEnumerable<Guid>> GetPermissionIdsByRoleIdAsync(Guid roleId, CancellationToken ct)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.PermissionId)
            .ToListAsync(ct);
    }

    public async Task<ApplicationRole?> GetAggregateAsync(Guid roleId, CancellationToken ct)
    {
        return await _context.Roles
            .IgnoreQueryFilters()
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == roleId, ct);
    }

    public async Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync(CancellationToken ct = default)
    {
        return await _context.RolePermissions
            .ToListAsync(ct);
    }
}
