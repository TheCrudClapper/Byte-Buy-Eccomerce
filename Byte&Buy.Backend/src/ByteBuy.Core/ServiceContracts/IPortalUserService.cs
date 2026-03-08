using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.PortalUser;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPortalUserService
    : IBaseCrudService<Guid, PortalUserAddRequest, PortalUserUpdateRequest, PortalUserResponse>
{
    Task<Result<PagedList<PortalUserListResponse>>> GetPortalUsersListAsync(
        PortalUserListQuery queryParams, CancellationToken ct = default);
    Task<Result<UserBasicInfoResponse>> GetBasicInfoAsync(Guid userId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> UpdateBasicInfoAsync(Guid userId, UserBasicInfoUpdateRequest request);
}
