using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class PermissionService(ICompanyPermissionHttpClient permissionHttpClient) : IPermissionService
{
    public Task<Result> DeleteByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await permissionHttpClient.GetSelectListAsync();
}