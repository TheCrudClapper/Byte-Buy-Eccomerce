using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RoleService(ICompanyRoleHttpClient roleClient) : IRoleService
{
    public async Task<Result<CreatedResponse>> AddAsync(RoleAddRequest request)
        => await roleClient.PostAsync(request);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RoleUpdateRequest request)
        => await roleClient.PutAsync(id, request);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await roleClient.GetSelectListAsync();

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid id)
        => await roleClient.GetByIdAsync(id);

    public async Task<Result<PagedList<RoleListResponse>>> GetListAsync(RoleListQuery query)
        => await roleClient.GetListAsync(query);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await roleClient.DeleteByIdAsync(id);
}
