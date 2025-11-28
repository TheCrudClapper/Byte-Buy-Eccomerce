using ByteBuy.Services.ModelsUI.Employee;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRoleService
{
    Task<Result<IEnumerable<RoleListItem>>> GetList();
}
