using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PortalUserService(IPortalUserHttpClient portalUserHttpClient) : IPortalUserService
{
    public async Task<Result<IEnumerable<PortalUserListResponse>>> GetList()
        => await portalUserHttpClient.GetListAsync();
}