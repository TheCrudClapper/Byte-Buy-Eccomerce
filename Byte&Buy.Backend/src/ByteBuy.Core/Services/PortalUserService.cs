using ByteBuy.Core.Domain.Carts;
using ByteBuy.Core.Domain.Carts.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Roles;
using ByteBuy.Core.Domain.Roles.Errors;
using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Core.Domain.Users.Errors;
using ByteBuy.Core.DTO.Public.ApplicationUser;
using ByteBuy.Core.DTO.Public.PortalUser;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Extensions;
using ByteBuy.Core.Filtration.PortalUser;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using static ByteBuy.Core.Specification.CartSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class PortalUserService : IPortalUserService

{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;
    private readonly ICartRepository _cartRepository;
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOfferRepository _offerRepository;
    private readonly IAddressValidationService _addressValidator;
    private readonly RoleManager<ApplicationRole> _roleManager;
    public PortalUserService(
        IPortalUserRepository portalUserRepository,
        IUserRepository userRepository,
        ICartRepository cartRepository,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IAddressValidationService addressValidator,
        IPasswordService passwordService,
        IOfferRepository offerRepository,
        IUnitOfWork unitOfWork)
    {
        _portalUserRepository = portalUserRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _offerRepository = offerRepository;
        _unitOfWork = unitOfWork;
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
        if (role is null || role.Name is null)
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

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (!identityResult.Succeeded)
                return identityResult.ToResult<CreatedResponse>();

            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
                return roleResult.ToResult<CreatedResponse>();

            await _cartRepository.AddAsync(cartResult.Value);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();
            return user.ToCreatedResponse();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure<CreatedResponse>(PortalUserErrors.PortalUserCreationFailed);
        }

    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, PortalUserUpdateRequest request)
    {
        var spec = new PortalUserWithUserPermissionsSpec(id);
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

        await _unitOfWork.BeginTransactionAsync();

        try
        {
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
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            return user.ToUpdatedResponse();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure<UpdatedResponse>(PortalUserErrors.PortalUserUpdateFailed);
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var spec = new PortalUserAggregateAndUserPermissionSpec(id);
        var portalUser = await _portalUserRepository.GetBySpecAsync(spec);
        if (portalUser is null)
            return Result.Failure(CommonUserErrors.NotFound);

        var cartSpec = new UserCartAggregateSpec(portalUser.Id);
        var userCart = await _cartRepository.GetBySpecAsync(cartSpec);
        if (userCart is null)
            return Result.Failure(CartErrors.NotFound);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            portalUser.Deactivate();
            userCart.Deactivate();

            var userOffers = await _offerRepository.GetOffersCreatedByUser(portalUser.Id);
            foreach (var offer in userOffers)
            {
                offer.Deactivate();
                await _offerRepository.UpdateAsync(offer);
            }

            await _portalUserRepository.UpdateAsync(portalUser);
            await _cartRepository.UpdateAsync(userCart);

            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            return Result.Failure(PortalUserErrors.PortalUserDeletionFailed);
        }

    }

    public async Task<Result<PortalUserResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var spec = new PortalUserReponseSpec(id);
        var portalUserDto = await _portalUserRepository.GetBySpecAsync(spec, ct);

        return portalUserDto is null
            ? Result.Failure<PortalUserResponse>(Error.NotFound)
            : portalUserDto;
    }

    public async Task<Result<PagedList<PortalUserListResponse>>> GetPortalUsersListAsync(
        PortalUserListQuery queryParams, CancellationToken ct = default)
    {
        return await _portalUserRepository.GetPortalUserPagedListAsync(queryParams, ct);
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

    public async Task<Result<UserBasicInfoResponse>> GetBasicInfoAsync(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserBasicInfoResponseSpec(userId);
        var userInfo = await _portalUserRepository.GetBySpecAsync(spec, ct);

        return userInfo is null
            ? Result.Failure<UserBasicInfoResponse>(CommonUserErrors.NotFound)
            : userInfo;
    }

    public async Task<Result<UpdatedResponse>> UpdateBasicInfoAsync(Guid userId, UserBasicInfoUpdateRequest request)
    {
        var spec = new PortalUserSpec(userId);
        var portalUser = await _portalUserRepository.GetBySpecAsync(spec);
        if (portalUser is null)
            return Result.Failure<UpdatedResponse>(CommonUserErrors.NotFound);

        if (portalUser.Email != request.Email && await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<UpdatedResponse>(AuthErrors.EmailAlreadyTaken);

        var updateResult = portalUser.UpdateBasicInfo(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber);

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _userManager.UpdateAsync(portalUser);
        return portalUser.ToUpdatedResponse();
    }
}
