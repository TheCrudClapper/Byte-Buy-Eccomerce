using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRoleService : IBaseService
{
    Task<Result<CreatedResponse>> AddAsync(RoleAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RoleUpdateRequest request);
    Task<Result<RoleResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<RoleListResponse>>> GetListAsync(RoleListQuery query);
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
}
