using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IPermissionService : ISelectableService<Guid>
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
}
