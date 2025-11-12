namespace ByteBuy.Core.ServiceContracts;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionName);
}
