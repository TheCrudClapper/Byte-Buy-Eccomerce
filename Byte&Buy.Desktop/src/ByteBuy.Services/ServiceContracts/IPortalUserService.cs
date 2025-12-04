using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPortalUserService
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetList();
    Task<Result<PortalUserResponse>> GetById(Guid userId);
    Task<Result<CreatedResponse>> Add(PortalUserAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid userId, PortalUserUpdateRequest request);
    Task<Result> DeleteById(Guid userId);
}