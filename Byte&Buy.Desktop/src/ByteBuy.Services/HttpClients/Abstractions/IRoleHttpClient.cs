using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IRoleHttpClient
{
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RoleUpdateRequest request);
    Task<Result<CreatedResponse>> AddAsync(RoleAddRequest request);
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync();
    Task<Result<IEnumerable<RoleResponse>>> GetAllAsync();
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<RoleResponse>> GetByIdAsync(Guid id);
}