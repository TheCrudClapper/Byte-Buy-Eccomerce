using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.Filtration.PortalUser;
using ByteBuy.Core.Pagination;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPortalUserRepository : IRepositoryBase<PortalUser>
{
    Task<PagedList<PortalUserListResponse>> GetPortalUserPagedListAsync(
        PortalUserListQuery queryParams, CancellationToken ct = default);
}
