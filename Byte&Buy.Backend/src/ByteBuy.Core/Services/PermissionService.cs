using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    public PermissionService(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }


    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        //true -> grant
        //false -> deny

        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        //if permision of given name not found -> deny access
        if (permission == null)
            return false;

        var userPerm = await _permissionRepository
            .CheckUserPermissionGrant(userId, permission.Id);

        //if user has grant override -> allow access
        if(userPerm)
            return true;

        //if user has explicit isGranted == false -> deny access
        var userPermFalse = await _permissionRepository
            .CheckUserPermissionNotGrant(userId, permission.Id);

        if (userPermFalse)
            return false;

        //get user role ids
        var userRoleId = await _permissionRepository.GetUserRoleId(userId);
        if (userRoleId is null) return false;

        //check if role of user has access
        return await _permissionRepository.CheckIfRoleHasPermission(userRoleId.Value, permission.Id);
    }
}
