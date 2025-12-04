using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PortalUserRepository : BaseRepository, IPortalUserRepository
{
    public PortalUserRepository(ApplicationDbContext context) : base(context){}

    public async Task<PortalUser?> GetPortalUserWithAllDataByIdAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
            .Include(p => p.UserPermissions)
            .Include(p => p.Addresses)
            .FirstOrDefaultAsync(p => p.Id == userId, ct);
    }
    public async Task<PortalUser?> GetPortalUserWithAddress(Guid userId, CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .Include(p => p.UserPermissions)
            .Include(p => p.Addresses)
            .FirstOrDefaultAsync(p => p.Id == userId, ct);
    }
    public async Task<IEnumerable<PortalUser>> GetPortalUsersWithRolesAsync(CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .Include(p => p.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync(ct);
    }

    public async Task UpdateAsync(PortalUser portalUser)
    {
        _context.PortalUsers.Update(portalUser);
        await _context.SaveChangesAsync();
    }
}
