using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPortalUserService
    : IBaseCrudService<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetPortalUsersListAsync(CancellationToken ct = default);
}
