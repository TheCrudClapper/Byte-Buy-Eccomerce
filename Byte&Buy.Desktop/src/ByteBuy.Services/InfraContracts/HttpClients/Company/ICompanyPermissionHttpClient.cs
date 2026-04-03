using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyPermissionHttpClient
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<CreatedResponse>> PostAsync(PermissionAddRequest request);
    Task<Result<UpdatedResponse>> PutAsync(Guid id, PermissionUpdateRequest request);
    Task<Result<PermissionResponse>> GetByIdAsync(Guid id);
    Task<Result<IReadOnlyCollection<PermissionResponse>>> GetListAsync();
    Task<Result> DeleteAsync(Guid id);
}
