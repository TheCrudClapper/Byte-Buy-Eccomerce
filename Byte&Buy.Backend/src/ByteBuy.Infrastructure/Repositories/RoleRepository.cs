using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    public RoleRepository(ApplicationDbContext context,
        RoleManager<ApplicationRole> roleManager) : base(context)
    {
        _roleManager = roleManager;
    }

    public async Task AddAsync(ApplicationRole role, CancellationToken ct)
    {
        await _roleManager.CreateAsync(role);
    }

    public async Task<bool> ExistsAsync(string roleName, CancellationToken ct)
    {
        return await _roleManager.Roles
            .AnyAsync(r => r.Name == roleName, ct);
    }

    public async Task<IEnumerable<ApplicationRole>> GetAllAsync(CancellationToken ct)
    {
        return await _roleManager.Roles
            .ToListAsync(ct);
    }

    public async Task<ApplicationRole?> GetByIdAsync(Guid roleId, CancellationToken ct)
    {
        return await _roleManager.Roles
            .FirstOrDefaultAsync(r => r.Id == roleId, ct);
    }

    public async Task UpdateAsync(ApplicationRole role, CancellationToken ct)
    {
        await _roleManager.UpdateAsync(role);
    }
}
