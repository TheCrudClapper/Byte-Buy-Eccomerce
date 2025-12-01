using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRoleService
{
    Task<Result<CreatedResponse>> Add(RoleAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, RoleUpdateRequest request);
    Task<Result<RoleResponse>> GetById(Guid id);
    Task<Result<IEnumerable<RoleResponse>>> GetAll();
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(Guid id);
    Task<Result> DeleteById(Guid id);
}
