using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PortalUserService(IPortalUserHttpClient portalUserHttpClient) : IPortalUserService
{

    public async Task<Result<CreatedResponse>> Add(PortalUserAddRequest request)
        => await portalUserHttpClient.PostPortalUserAsync(request);

    public async Task<Result> DeleteById(Guid userId)
        => await portalUserHttpClient.DeleteByIdAsync(userId);

    public async Task<Result<PortalUserResponse>> GetById(Guid userId)
        => await portalUserHttpClient.GetByIdAsync(userId);

    public async Task<Result<IEnumerable<PortalUserListResponse>>> GetList()
        => await portalUserHttpClient.GetListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid userId, PortalUserUpdateRequest request)
        => await portalUserHttpClient.PutPortalUserAsync(userId, request);
}