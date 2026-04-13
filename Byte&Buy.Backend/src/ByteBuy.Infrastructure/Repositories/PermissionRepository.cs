using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.Filtration.Permission;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PermissionRepository : EfBaseRepository<Permission>, IPermissionRepository
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

    public async Task<Permission?> GetByNameAsync(string name, CancellationToken ct)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Name == name, ct);
    }

    public async Task<bool> ExistsWithNameAsync(string name, Guid? excludedId)
    {
        return await _context.Permissions
            .AnyAsync(p => p.Name == name && excludedId != p.Id);
    }

    public async Task<bool> HasActiveRelations(Guid permissionId)
    {
        return await _context.Permissions
            .AnyAsync(p => p.RolePermissions
                .Any(rp => rp.PermissionId == permissionId)
                || p.UserPermissions.Any(up => up.PermissionId == permissionId));
    }

    public async Task<PagedList<PermissionResponse>> GetPermissionListAsync(PermissionListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Permissions
            .AsNoTracking()
            .OrderByDescending(o => o.DateCreated)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Name))
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{queryParams.Name}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.Description))
            query = query.Where(p => EF.Functions.ILike(p.Description, $"%{queryParams.Description}%"));

        var projection = query.Select(PermissionMappings.PermisionResponseProjection);

        return await projection
            .ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }
}
