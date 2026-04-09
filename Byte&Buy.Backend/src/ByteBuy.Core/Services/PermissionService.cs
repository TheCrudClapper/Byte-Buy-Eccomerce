using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.Permissions.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Permission;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.PermissionSpecifications;
namespace ByteBuy.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public PermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
    {
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct = default)
    {
        var spec = new PermissionSelectListItemSpec();
        return await _permissionRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
    {
        var permission = await _permissionRepository.GetByNameAsync(permissionName);
        //if permision of given name not found -> deny access
        if (permission is null) return false;

        return await _permissionRepository
            .HasUserOrRolePermissionAsync(userId, permission.Id);
    }

    public async Task<Result<CreatedResponse>> AddAsync(PermissionAddRequest request)
    {
        var exists = await _permissionRepository.ExistsWithNameAsync(request.Name);
        if (exists)
            return Result.Failure<CreatedResponse>(PermissionErrors.AlreadyExists);

        var result = Permission.Create(request.Name, request.Description);
        if (result.IsFailure)
            return Result.Failure<CreatedResponse>(result.Error);

        await _permissionRepository.AddAsync(result.Value);
        await _unitOfWork.SaveChangesAsync();

        return result.Value.ToCreatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var hasActiveRelations = await _permissionRepository.HasActiveRelations(id);
        if (hasActiveRelations)
            return Result.Failure(PermissionErrors.HasActiveRelations);

        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission is null)
            return Result.Failure(PermissionErrors.NotFound);

        permission.Deactivate();
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<PermissionResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new PermissionResponseSpec(id);
        var dto = await _permissionRepository.GetBySpecAsync(spec, ct);
        return dto is null
            ? Result.Failure<PermissionResponse>(PermissionErrors.NotFound)
            : dto;
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PermissionUpdateRequest request)
    {
        var exists = await _permissionRepository.ExistsWithNameAsync(request.Name, id);
        if (exists)
            return Result.Failure<UpdatedResponse>(PermissionErrors.AlreadyExists);

        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission is null)
            return Result.Failure<UpdatedResponse>(PermissionErrors.NotFound);

        var result = permission.Update(request.Name, request.Description);

        if (result.IsFailure)
            return Result.Failure<UpdatedResponse>(result.Error);

        await _unitOfWork.SaveChangesAsync();
        return permission.ToUpdatedResponse();
    }

    public async Task<Result<IReadOnlyCollection<PermissionResponse>>> GetPermissionListAsync(CancellationToken ct = default)
        => Result.Success(await _permissionRepository.GetPermissionListAsync(ct));

}