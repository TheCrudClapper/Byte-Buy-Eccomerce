using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Filtration.PortalUser;

namespace ByteBuy.Infrastructure.Repositories;

public class PortalUserRepository : EfBaseRepository<PortalUser>, IPortalUserRepository
{
    public PortalUserRepository(ApplicationDbContext context) : base(context) { }

    public async Task<PagedList<PortalUserListResponse>> GetPortalUserPagedListAsync(
        PortalUserListQuery queryParams, CancellationToken ct = default)
    {
        var query = _context.PortalUsers
            .AsNoTracking()
            .OrderByDescending(p => p.DateCreated)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.FirstName))
            query = query.Where(e => EF.Functions.ILike(e.FirstName, $"%{queryParams.FirstName}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.Email))
            query = query.Where(e => EF.Functions.ILike(e.Email!, $"%{queryParams.Email}%"));

        if (!string.IsNullOrWhiteSpace(queryParams.LastName))
            query = query.Where(e => EF.Functions.ILike(e.LastName, $"%{queryParams.LastName}%"));

        var projection = query.Select(PortalUserMappings.PortalUserListResponseProjection);

        return await projection.ToPagedListAsync(queryParams.PageNumber, queryParams.PageSize, ct);
    }
}
