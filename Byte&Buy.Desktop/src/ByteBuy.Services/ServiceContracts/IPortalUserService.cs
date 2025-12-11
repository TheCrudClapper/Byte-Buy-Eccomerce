using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPortalUserService : IBaseService
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetList();
    Task<Result<PortalUserResponse>> GetById(Guid id);
    Task<Result<CreatedResponse>> Add(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, PortalUserUpdateRequest request);
}