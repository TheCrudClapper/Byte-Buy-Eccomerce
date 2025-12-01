using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RoleService(IRoleHttpClient roleClient) 
    : CrudServiceBase<RoleResponse, RoleAddRequest, RoleUpdateRequest> ,IRoleService
{
    public override async Task<Result<RoleResponse>> Add(RoleAddRequest request)
        => await roleClient.AddAsync(request);
    public override async Task<Result<RoleResponse>> Update(Guid id, RoleUpdateRequest request)
        => await roleClient.UpdateAsync(id, request);

    public override async Task<Result<IEnumerable<RoleResponse>>> GetAll()
        => await roleClient.GetAllAsync();

    public override async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await roleClient.GetSelectListItemsAsync();

    public override async Task<Result<RoleResponse>> GetById(Guid id)
        => await roleClient.GetByIdAsync(id);

    public override async Task<Result> DeleteById(Guid id)
        => await roleClient.DeleteByIdAsync(id);
}
