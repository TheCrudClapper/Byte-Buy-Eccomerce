using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPortalUserService : IBaseService
{
    Task<Result<PagedList<PortalUserListResponse>>> GetList(PortalUserListQuery query);
    Task<Result<PortalUserResponse>> GetById(Guid id);
    Task<Result<CreatedResponse>> Add(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, PortalUserUpdateRequest request);
}