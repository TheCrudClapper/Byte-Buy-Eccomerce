using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class PortalUserRepository : EfBaseRepository<PortalUser>, IPortalUserRepository
{
    public PortalUserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PortalUser?> GetAggregateAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .IgnoreQueryFilters()
            .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
            .Include(p => p.UserPermissions)
            .Include(p => p.Addresses)
            .FirstOrDefaultAsync(p => p.Id == userId, ct);
    }
}
