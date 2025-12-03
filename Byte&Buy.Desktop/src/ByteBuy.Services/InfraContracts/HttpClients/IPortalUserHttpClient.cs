using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IPortalUserHttpClient
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetListAsync();
}