using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Role;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public RoleService(IRoleRepository roleRepository,
        RoleManager<ApplicationRole> roleManager)
    {
        _roleRepository = roleRepository;
        _roleManager = roleManager;
    }

    public async Task<Result<CreatedResponse>> AddRole(RoleAddRequest request)
    {
        if (await _roleRepository.ExistsAsync(request.Name))
            return Result.Failure<CreatedResponse>(RoleErrors.RoleAlreadyExist);

        var roleResult = ApplicationRole
            .Create(request.Name, request.PermissionIds);

        if (roleResult.IsFailure)
            return Result.Failure<CreatedResponse>(roleResult.Error);

        var role = roleResult.Value;

        var roleCreation = await _roleManager.CreateAsync(role);
        if (!roleCreation.Succeeded)
            return roleCreation.ToResult<CreatedResponse>();

        return role.ToCreatedResponse();
    }

    public async Task<Result> DeleteRole(Guid roleId)
    {
        if (await _roleRepository.DoesRoleHaveActiveUsers(roleId))
            return Result.Failure(RoleErrors.RoleHasActiveUsers);

        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role is null)
            return Result.Failure(Error.NotFound);

        role.Deactivate();

        var updationResult = await _roleManager.UpdateAsync(role);
        if (!updationResult.Succeeded)
            return updationResult.ToResult<RoleResponse>();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRoles(CancellationToken ct = default)
    {
        var roles = await _roleRepository.GetAllAsync(ct);
        var rolePermissions = await _roleRepository.GetAllRolePermissionsAsync(ct);

        var groupedPermissions = rolePermissions
            .GroupBy(rp => rp.RoleId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.PermissionId).ToList());

        var roleDtos = roles.Select(role =>
        {
            groupedPermissions.TryGetValue(role.Id, out var permissions);
            return role.ToRoleResponse(permissions ?? []);
        })
        .ToList();

        return roleDtos;
    }

    public async Task<Result<RoleResponse>> GetRole(Guid roleId, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(roleId, ct);
        if (role is null)
            return Result.Failure<RoleResponse>(Error.NotFound);

        var permissionIds = await _roleRepository.GetPermissionIdsByRoleIdAsync(roleId);

        return role.ToRoleResponse(permissionIds);
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct)
    {
        var roles = await _roleRepository.GetAllAsync(ct);

        return roles.Select(item => item.ToSelectListItemResponse())
            .ToList();
    }

    public async Task<Result<UpdatedResponse>> UpdateRole(Guid roleId, RoleUpdateRequest request)
    {
        var role = await _roleRepository.GetAggregateAsync(roleId);
        if (role is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var roleResult = role.Update(request.Name);
        if (roleResult.IsFailure)
            return Result.Failure<UpdatedResponse>(roleResult.Error);

        var permissionResult = role.SetPermissions(request.PermissionIds);
        if (permissionResult.IsFailure)
            return Result.Failure<UpdatedResponse>(permissionResult.Error);

        var identityUpdate = await _roleManager.UpdateAsync(role);
        if (!identityUpdate.Succeeded)
            return identityUpdate.ToResult<UpdatedResponse>();

        return role.ToUpdatedResponse();
    }
}
