using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class PortalUserHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), IPortalUserHttpClient
{
    public async Task<Result<IEnumerable<PortalUserListResponse>>> GetListAsync()
        => await GetAsync<IEnumerable<PortalUserListResponse>>("portalusers/list");
}