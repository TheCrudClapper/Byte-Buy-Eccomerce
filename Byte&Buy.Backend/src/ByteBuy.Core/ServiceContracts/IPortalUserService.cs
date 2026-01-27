using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPortalUserService
    : IBaseCrudService<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    Task<Result<IReadOnlyCollection<PortalUserListResponse>>> GetPortalUsersListAsync(CancellationToken ct = default);
    Task<Result<UserBasicInfoResponse>> GetBasicUserInfoAsync(Guid userId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> PutUserBasicInfo(Guid userId, UserBasicInfoUpdateRequest request);
}
