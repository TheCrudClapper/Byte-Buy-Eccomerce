using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Role;
using ByteBuy.Services.ModelsUI.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Abstractions;

public interface IRoleHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListItemsAsync();
    Task<Result<IEnumerable<RoleResponse>>> GetAllAsync();
}