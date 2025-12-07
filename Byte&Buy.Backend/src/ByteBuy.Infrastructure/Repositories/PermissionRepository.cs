using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PermissionRepository : BaseRepository, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> HasUserOrRolePermissionAsync(Guid userId, Guid permissionId)
    {
        //true -> grant
        //false -> deny
        //null -> not found, check roles instead
        var userPermission = await _context.UserPermissions
            .AsNoTracking()
            .Where(up => up.UserId == userId && up.PermissionId == permissionId)
            .Select(up => (bool?)up.IsGranted)
            .FirstOrDefaultAsync();

        if (userPermission.HasValue)
            return userPermission.Value;

        var hasRolePermission = await _context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .AnyAsync(rp => rp.PermissionId == permissionId);

        return hasRolePermission;
    }

    public async Task<Permission?> GetByIdAsync(Guid permissionId, CancellationToken ct)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == permissionId, ct);
    }

    public async Task<Permission?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Name == name, ct);
    }

    public async Task<IEnumerable<Permission>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Permissions
            .ToListAsync(ct);
    }
}
