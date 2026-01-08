using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using static ByteBuy.Core.Specification.CartSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class PortalUserService : IPortalUserService
{
    private readonly IPasswordService _passwordService;
    private readonly ICartRepository _cartRepository;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAddressValidationService _addressValidator;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public PortalUserService(
        IPortalUserRepository portalUserRepository,
        IUserRepository userRepository,
        ICartRepository cartRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IAddressValidationService addressValidator,
        IPasswordService passwordService)
    {
        _portalUserRepository = portalUserRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _addressValidator = addressValidator;
        _cartRepository = cartRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<CreatedResponse>> AddAsync(PortalUserAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<CreatedResponse>(AuthErrors.EmailAlreadyTaken);

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        var userResult = PortalUser.CreateWithAddress(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.HomeAddress.Street,
            request.HomeAddress.HouseNumber,
            request.HomeAddress.PostalCity,
            request.HomeAddress.PostalCode,
            request.HomeAddress.City,
            request.HomeAddress.Country,
            request.HomeAddress.FlatNumber,
            request.RevokedPermissionIds,
            request.GrantedPermissionIds,
            _addressValidator);

        if (userResult.IsFailure)
            return Result.Failure<CreatedResponse>(userResult.Error);

        var user = userResult.Value;

        var cartResult = Cart.Create(user.Id);
        if (cartResult.IsFailure)
            return Result.Failure<CreatedResponse>(cartResult.Error);

        user.AttachCart(cartResult.Value.Id);

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
            return identityResult.ToResult<CreatedResponse>();

        var roleResult = await _userManager.AddToRoleAsync(user, role.Name ?? "");
        if (!roleResult.Succeeded)
            return roleResult.ToResult<CreatedResponse>();

        return user.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PortalUserUpdateRequest request)
    {
        var spec = new PortalUserWithPermissionsSpec(id);
        var user = await _portalUserRepository.GetBySpecAsync(spec);

        if (user is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (newRole is null)
            return Result.Failure<UpdatedResponse>(RoleErrors.NotFound);

        if (user.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<UpdatedResponse>(AuthErrors.EmailAlreadyTaken);

        var updateResult = user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.GrantedPermissionIds,
            request.RevokedPermissionIds);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var validation = await _passwordService.ValidateAsync(user, request.Password);
            if (!validation.Succeeded)
                return validation.ToResult<UpdatedResponse>();

            var change = await _passwordService.ChangePasswordAsync(user, request.Password);
            if (!change.Succeeded)
                return change.ToResult<UpdatedResponse>();
        }

        var roleChange = await UpdateUserRoleAsync(user, newRole);
        if (roleChange.IsFailure)
            return Result.Failure<UpdatedResponse>(roleChange.Error);

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();

        return user.ToUpdatedResponse();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var spec = new PortalUserWithAddressAndPermissionSpec(id);
        var portalUser = await _portalUserRepository.GetBySpecAsync(spec);
        if (portalUser is null)
            return Result.Failure(Error.NotFound);

        var cartSpec = new CartAggregateByUserIdSpec(portalUser.Id);
        var userCart = await _cartRepository.GetBySpecAsync(cartSpec);
        if (userCart is null)
            return Result.Failure(CartErrors.NotFound);

        portalUser.Deactivate();
        userCart.Deactivate();

        await _portalUserRepository.UpdateAsync(portalUser);
        await _cartRepository.UpdateAsync(userCart);
        await _portalUserRepository.CommitAsync();

        return Result.Success();
    }

    public async Task<Result<PortalUserResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new PortalUserToPortalUserReponseSpec(id);
        var portalUserDto = await _portalUserRepository.GetBySpecAsync(spec, ct);

        return portalUserDto is null
            ? Result.Failure<PortalUserResponse>(Error.NotFound)
            : portalUserDto;
    }

    public async Task<Result<IReadOnlyCollection<PortalUserListResponse>>> GetPortalUsersListAsync(CancellationToken ct = default)
    {
        var spec = new PortalUserToPortalUserListResponseSpec();
        return await _portalUserRepository.GetListBySpecAsync(spec, ct);
    }

    private async Task<Result> UpdateUserRoleAsync(ApplicationUser user, ApplicationRole newRole)
    {
        var currentRoles = await _userManager.GetRolesAsync(user);
        var currentRole = currentRoles.SingleOrDefault();

        if (currentRole == newRole.Name)
            return Result.Success();

        if (currentRole is not null)
        {
            var remove = await _userManager.RemoveFromRoleAsync(user, currentRole);
            if (!remove.Succeeded)
                return remove.ToResult();
        }

        var add = await _userManager.AddToRoleAsync(user, newRole.Name!);
        if (!add.Succeeded)
            return add.ToResult();

        return Result.Success();
    }
}
