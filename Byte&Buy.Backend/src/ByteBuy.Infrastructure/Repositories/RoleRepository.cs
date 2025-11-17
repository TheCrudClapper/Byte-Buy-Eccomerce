using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context){}

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
            .FirstOrDefaultAsync(r => r.Id == roleId, ct);
    }
}
