using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PermissionService(ICompanyPermissionHttpClient permissionHttpClient) : IPermissionService
{
    public async Task<Result<CreatedResponse>> AddAsync(PermissionAddRequest request)
        => await permissionHttpClient.PostAsync(request);

    public async Task<Result> DeleteAsync(Guid id)
        => await permissionHttpClient.DeleteAsync(id);

    public async Task<Result> DeleteByIdAsync(Guid id)
         => await permissionHttpClient.DeleteAsync(id);

    public async Task<Result<PermissionResponse>> GetByIdAsync(Guid id)
        => await permissionHttpClient.GetByIdAsync(id);

    public async Task<Result<IReadOnlyCollection<PermissionResponse>>> GetListAsync()
        => await permissionHttpClient.GetListAsync();

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await permissionHttpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PermissionUpdateRequest request)
        => await permissionHttpClient.PutAsync(id, request);
}