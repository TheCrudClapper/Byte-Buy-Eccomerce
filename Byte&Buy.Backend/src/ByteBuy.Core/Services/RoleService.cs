using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Role;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Filtration.Role;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using static ByteBuy.Core.Specification.RoleSpecifications;

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

    public async Task<Result<CreatedResponse>> AddAsync(RoleAddRequest request)
    {
        if (await _roleRepository.ExistsByNameAsync(request.Name))
            return Result.Failure<CreatedResponse>(RoleErrors.AlreadyExist);

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

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, RoleUpdateRequest request)
    {
        var spec = new RoleAndRolePermissionsSpec(id);
        var role = await _roleRepository.GetBySpecAsync(spec);
        if (role is null)
            return Result.Failure<UpdatedResponse>(RoleErrors.NotFound);

        var roleResult = role.Update(request.Name, request.PermissionIds);
        if (roleResult.IsFailure)
            return Result.Failure<UpdatedResponse>(roleResult.Error);

        var identityUpdate = await _roleManager.UpdateAsync(role);
        if (!identityUpdate.Succeeded)
            return identityUpdate.ToResult<UpdatedResponse>();

        return role.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        if (await _roleRepository.DoesRoleHaveActiveUsers(id))
            return Result.Failure(RoleErrors.HasActiveUsers);

        var spec = new RoleAndRolePermissionsSpec(id, false);
        var role = await _roleRepository.GetBySpecAsync(spec);

        if (role is null)
            return Result.Failure(RoleErrors.NotFound);

        role.Deactivate();

        var updationResult = await _roleManager.UpdateAsync(role);
        if (!updationResult.Succeeded)
            return updationResult.ToResult<RoleResponse>();

        return Result.Success();
    }

    public async Task<Result<PagedList<RoleListResponse>>> GetRolesListAsync(RoleListQuery queryParams, CancellationToken ct = default)
    {
        return await _roleRepository.GetPagedRoleListAsync(queryParams, ct);
    }

    public async Task<Result<RoleResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new RoleResponseSpec(id);
        var roleDto = await _roleRepository.GetBySpecAsync(spec, ct);

        return roleDto is null
            ? Result.Failure<RoleResponse>(RoleErrors.NotFound)
            : roleDto;
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
    {
        var spec = new SelectListItemResponseSpec();
        return await _roleRepository.GetListBySpecAsync(spec, ct);
    }
}
