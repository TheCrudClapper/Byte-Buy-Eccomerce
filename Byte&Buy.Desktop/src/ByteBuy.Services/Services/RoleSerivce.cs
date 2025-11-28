using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.Mappings;
using ByteBuy.Services.ModelsUI.Employee;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RoleService(IRoleHttpClient roleClient) 
    : CrudServiceBase<RoleResponse, RoleAddRequest, RoleUpdateRequest> ,IRoleService
{
    public override Task<Result<RoleResponse>> Add(RoleAddRequest request)
        => throw new NotImplementedException();

    public override Task<Result<RoleResponse>> Update(Guid id, RoleUpdateRequest request)
        => throw new NotImplementedException();

    public override async Task<Result<IEnumerable<RoleResponse>>> GetAll()
        => await roleClient.GetAllAsync();

    public override async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await roleClient.GetSelectListItemsAsync();

    public override Task<Result<RoleResponse>> GetById(Guid id)
        => throw new NotImplementedException();

    public override Task<Result> DeleteById(Guid id)
        => throw new NotImplementedException();

    public async Task<Result<IEnumerable<RoleListItem>>> GetList()
    {
        var result = await roleClient.GetAllAsync();
        if (!result.Success)
            return Result<IEnumerable<RoleListItem>>.Fail(result.Error!);

        var roles = result.Value!.Select(r => r.ToRoleListItem())
            .ToList();

        return Result<IEnumerable<RoleListItem>>.Ok(roles);
    }
}
