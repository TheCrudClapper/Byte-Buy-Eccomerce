using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IPortalUserService
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetList();
}