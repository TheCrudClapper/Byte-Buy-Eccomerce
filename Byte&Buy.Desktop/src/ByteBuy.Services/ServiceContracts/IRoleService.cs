using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRoleService : IBaseService
{
    Task<Result<CreatedResponse>> Add(RoleAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, RoleUpdateRequest request);
    Task<Result<RoleResponse>> GetById(Guid id);
    Task<Result<PagedList<RoleListResponse>>> GetList(RoleListQuery query);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList();
}
