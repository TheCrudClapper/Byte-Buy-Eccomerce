using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IPortalUserService
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct = default);
    Task<Result<CreatedResponse>> AddPortalUser(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> UpdatePortalUser(Guid userId, PortalUserUpdateRequest request);
    Task<Result> DeletePortalUser(Guid userId);
    Task<Result<PortalUserResponse>> GetPortalUser(Guid userId, CancellationToken ct = default);
}
