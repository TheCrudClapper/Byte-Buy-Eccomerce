using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyRoleHttpClient
{
    Task<Result<UpdatedResponse>> PutAsync(Guid id, RoleUpdateRequest request);
    Task<Result<CreatedResponse>> PostAsync(RoleAddRequest request);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListItemsAsync();
    Task<Result<PagedList<RoleListResponse>>> GetListAsync(RoleListQuery query);
    Task<Result> DeleteByIdAsync(Guid id);
    Task<Result<RoleResponse>> GetByIdAsync(Guid id);
}