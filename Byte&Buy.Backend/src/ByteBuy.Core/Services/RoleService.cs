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

    public async Task<Result<RoleResponse>> AddRole(RoleAddRequest request)
    {
        if (await _roleRepository.ExistsAsync(request.Name))
            return Result.Failure<RoleResponse>(RoleErrors.RoleAlreadyExist);

        var roleResult = ApplicationRole.Create(request.Name);

        if (roleResult.IsFailure)
            return Result.Failure<RoleResponse>(roleResult.Error);

        var role = roleResult.Value;

        var roleCreation =  await _roleManager.CreateAsync(role);
        if (!roleCreation.Succeeded)
            return roleCreation.ToResult<RoleResponse>();

        return role.ToRoleResponse();
    }

    public async Task<Result> DeleteRole(Guid roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role is null)
            return Result.Failure(Error.NotFound);

        role.Deactivate();

        var updationResult = await _roleManager.UpdateAsync(role);
        if(!updationResult.Succeeded)
            return updationResult.ToResult<RoleResponse>();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RoleResponse>>> GetAllRoles(CancellationToken ct = default)
    {
        var roles = await _roleRepository.GetAllAsync(ct);

        return roles.Select(r => r.ToRoleResponse())
            .ToList();
    }

    public async Task<Result<RoleResponse>> GetRole(Guid roleId, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(roleId, ct);
        if (role is null)
            return Result.Failure<RoleResponse>(Error.NotFound);

        return role.ToRoleResponse();
    }

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct)
    {
        var roles = await _roleRepository.GetAllAsync(ct);

        return roles.Select(item => item.ToSelectListItemResponse())
            .ToList();
    }

    public async Task<Result<RoleResponse>> UpdateRole(Guid roleId, RoleUpdateRequest request)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role is null)
            return Result.Failure<RoleResponse>(Error.NotFound);

        var roleResult = role.Update(request.Name);
        if(roleResult.IsFailure)
            return Result.Failure<RoleResponse>(roleResult.Error);

        var updationResult = await _roleManager.UpdateAsync(role);
        if (!updationResult.Succeeded)
            return updationResult.ToResult<RoleResponse>();

        return role.ToRoleResponse();
    }
}
