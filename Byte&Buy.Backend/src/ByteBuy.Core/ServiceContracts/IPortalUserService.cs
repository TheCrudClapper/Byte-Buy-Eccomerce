using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IPortalUserService
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct = default);
    Task<Result<CreatedResponse>> AddPortalUser(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> UpdatePortalUser(Guid userId, PortalUserAddRequest request);
    Task<Result<PortalUserResponse>> GetPortalUser(Guid userId, CancellationToken ct = default);
}
