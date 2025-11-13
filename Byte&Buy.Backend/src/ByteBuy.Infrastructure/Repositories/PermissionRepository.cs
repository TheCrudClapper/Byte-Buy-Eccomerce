using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PermissionRepository : BaseRepository, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context) : base(context){}

    public async Task<bool> CheckIfRoleHasPermission(Guid roleId, Guid permissionId, CancellationToken ct)
    {
        return await _context.RolePermissions
            .AnyAsync(ur => ur.RoleId == roleId && ur.PermissionId == permissionId && ur.IsActive, ct);
    }

    public async Task<bool> CheckUserPermissionGrant(Guid userId, Guid permissionId, CancellationToken ct)
    {
        return await _context.UserPermissions
            .Where(up => up.UserId == userId && up.PermissionId == permissionId && up.IsActive)
            .Select(up => up.IsGranted)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> CheckUserPermissionNotGrant(Guid userId, Guid permissionId, CancellationToken ct)
    {
        return await _context.UserPermissions
            .AnyAsync(up => up.UserId == userId 
            && up.PermissionId == permissionId 
            && up.IsGranted == false 
            && up.IsActive, ct);
    }

    public async Task<Permission?> GetByIdAsync(Guid permissionId, CancellationToken ct)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == permissionId && p.IsActive, ct);
    }

    public async Task<Permission?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Name == name && p.IsActive, ct);
    }

    public async Task<Guid?> GetUserRoleId(Guid userId, CancellationToken ct = default)
    {
        return await _context.UserRoles
            .Where(r => r.UserId == userId)
            .Select(r => r.RoleId)
            .FirstOrDefaultAsync(ct);
    }
}
