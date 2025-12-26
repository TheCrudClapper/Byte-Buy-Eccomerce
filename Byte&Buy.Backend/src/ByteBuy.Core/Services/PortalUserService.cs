using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class PortalUserService : IPortalUserService
{
    private readonly IAddressReadRepository _addressRepository;
    private readonly IPasswordService _passwordService;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAddressValidationService _addressValidator;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public PortalUserService(
        IPortalUserRepository portalUserRepository,
        IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ICountryRepository countryRepository,
        IAddressValidationService addressValidator,
        IPasswordService passwordService,
        IAddressReadRepository addressRepository)
    {
        _portalUserRepository = portalUserRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
        _passwordService = passwordService;
        _addressRepository = addressRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(PortalUserAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<CreatedResponse>(AuthErrors.AccountExists);

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null)
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.Address.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.NotFound);

        var addressResult = Address.Create(
            request.Address.Label,
            request.Address.City,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.PostalCity,
            request.Address.PostalCode,
            request.Address.FlatNumber,
            country,
            true,
            _addressValidator);

        if (addressResult.IsFailure)
            return Result.Failure<CreatedResponse>(addressResult.Error);

        var address = addressResult.Value;

        var userResult = PortalUser.CreateWithAddress(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            address,
            request.RevokedPermissionIds,
            request.GrantedPermissionIds);

        if (userResult.IsFailure)
            return Result.Failure<CreatedResponse>(userResult.Error);

        var user = userResult.Value;

        var cartResult = Cart.Create(user);
        if (cartResult.IsFailure)
            return Result.Failure<CreatedResponse>(cartResult.Error);

        user.AssignCart(cartResult.Value);

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
        var user = await _portalUserRepository.GetAggregateAsync(id);
        if (user is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (newRole is null)
            return Result.Failure<UpdatedResponse>(RoleErrors.NotFound);

        if (user.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<UpdatedResponse>(AuthErrors.AccountExists);

        var updateResult = user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber);

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

        var permissionResult = user.SetPermissionOverrides(request.RevokedPermissionIds, request.GrantedPermissionIds);
        if (permissionResult.IsFailure)
            return Result.Failure<UpdatedResponse>(permissionResult.Error);

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

        portalUser.Deactivate();

        await _portalUserRepository.UpdateAsync(portalUser);
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
