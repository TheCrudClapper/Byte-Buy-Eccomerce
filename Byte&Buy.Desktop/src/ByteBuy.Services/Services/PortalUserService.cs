using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PortalUserService(ICompanyPortalUserHttpClient portalUserHttpClient) : IPortalUserService
{

    public async Task<Result<CreatedResponse>> AddAsync(PortalUserAddRequest request)
        => await portalUserHttpClient.PostPortalUserAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid userId)
        => await portalUserHttpClient.DeleteByIdAsync(userId);

    public async Task<Result<PortalUserResponse>> GetByIdAsync(Guid userId)
        => await portalUserHttpClient.GetByIdAsync(userId);

    public async Task<Result<PagedList<PortalUserListResponse>>> GetListAsync(PortalUserListQuery query)
        => await portalUserHttpClient.GetListAsync(query);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid userId, PortalUserUpdateRequest request)
        => await portalUserHttpClient.PutPortalUserAsync(userId, request);
}