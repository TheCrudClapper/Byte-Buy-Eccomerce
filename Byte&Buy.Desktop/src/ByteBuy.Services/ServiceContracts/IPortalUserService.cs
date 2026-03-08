using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPortalUserService : IBaseService
{
    Task<Result<PagedList<PortalUserListResponse>>> GetListAsync(PortalUserListQuery query);
    Task<Result<PortalUserResponse>> GetByIdAsync(Guid id);
    Task<Result<CreatedResponse>> AddAsync(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PortalUserUpdateRequest request);
}