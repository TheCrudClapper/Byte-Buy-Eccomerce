using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PortalUserRepository : BaseRepository, IPortalUserRepository
{
    public PortalUserRepository(ApplicationDbContext context) : base(context){}

    public async Task<IEnumerable<PortalUser>> GetPortalUSersWithRolesAsync(CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .Include(p => p.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync(ct);
    }
}
