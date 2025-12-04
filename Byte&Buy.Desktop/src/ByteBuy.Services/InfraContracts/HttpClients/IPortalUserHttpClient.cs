using ByteBuy.Services.DTO.PortalUser;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IPortalUserHttpClient
{
    Task<Result<IEnumerable<PortalUserListResponse>>> GetListAsync();
    Task<Result<PortalUserResponse>> GetByIdAsync(Guid userId);
    Task<Result<CreatedResponse>> PostPortalUserAsync(PortalUserAddRequest request);
    Task<Result> DeleteByIdAsync(Guid userId);
    Task<Result<UpdatedResponse>> PutPortalUserAsync(Guid userId, PortalUserUpdateRequest request);
}