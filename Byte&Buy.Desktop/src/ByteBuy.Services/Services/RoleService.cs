using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class RoleService(IRoleHttpClient roleClient) : IRoleService
{
    public async Task<Result<CreatedResponse>> Add(RoleAddRequest request)
        => await roleClient.PostAsync(request);

    public async Task<Result<UpdatedResponse>> Update(Guid id, RoleUpdateRequest request)
        => await roleClient.PutAsync(id, request);

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await roleClient.GetSelectListItemsAsync();

    public async Task<Result<RoleResponse>> GetById(Guid id)
        => await roleClient.GetByIdAsync(id);

    public async Task<Result<IEnumerable<RoleResponse>>> GetAll()
        => await roleClient.GetAllAsync();

    public async Task<Result> DeleteById(Guid id)
        => await roleClient.DeleteByIdAsync(id);
}
