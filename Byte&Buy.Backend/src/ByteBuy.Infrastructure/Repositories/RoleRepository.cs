using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Extensions;
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

    public async Task<bool> ExistsByNameAsync(string roleName, CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name == roleName, ct);
    }

    public async Task<PagedList<RoleListResponse>> GetPagedRoleListAsync(RoleListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.Roles
            .AsNoTracking()
            .Where(r => !r.IsSystemRole)
            .OrderByDescending(r => r.DateCreated)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.RoleName))
            query = query.Where(r => EF.Functions.ILike(r.Name!, $"%{queryParams.RoleName}%"));

        var projection = query.Select(RoleMappings.RoleListResponseProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }
}
