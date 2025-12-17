using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IRoleHttpClient
{
    Task<Result<UpdatedResponse>> PutAsync(Guid id, RoleUpdateRequest request);
    Task<Result<CreatedResponse>> PostAsync(RoleAddRequest request);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListItemsAsync();
    Task<Result<IEnumerable<RoleResponse>>> GetAllAsync();
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<RoleResponse>> GetByIdAsync(Guid id);
}