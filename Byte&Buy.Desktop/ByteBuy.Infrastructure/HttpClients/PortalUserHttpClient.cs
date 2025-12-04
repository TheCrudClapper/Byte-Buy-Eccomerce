using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class PortalUserHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), IPortalUserHttpClient
{
    public async Task<Result<CreatedResponse>> PostPortalUserAsync(PortalUserAddRequest request)
        => await PostAsync<CreatedResponse>("portalusers", request);

    public async Task<Result> DeleteByIdAsync(Guid userId)
        => await DeleteAsync($"portalusers{userId}");

    public async Task<Result<PortalUserResponse>> GetByIdAsync(Guid userId)
        => await GetAsync<PortalUserResponse>($"portalusers/{userId}");

    public async Task<Result<IEnumerable<PortalUserListResponse>>> GetListAsync()
        => await GetAsync<IEnumerable<PortalUserListResponse>>("portalusers/list");

    public async Task<Result<UpdatedResponse>> PutPortalUserAsync(Guid userId, PortalUserUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"portalusers/{userId}", request);
}