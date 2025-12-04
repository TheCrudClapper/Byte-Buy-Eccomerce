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

namespace ByteBuy.Core.Services;

public class PortalUserService : IPortalUserService
{
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
        IAddressValidationService addressValidator)
    {
        _portalUserRepository = portalUserRepository;
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CreatedResponse>> AddPortalUser(PortalUserAddRequest request)
    {
        if (await _userRepository.ExistByEmailAsync(request.Email))
            return Result.Failure<CreatedResponse>(AuthErrors.AccountExists);

        var applicationRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (applicationRole is null)
            return Result.Failure<CreatedResponse>(RoleErrors.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.Address.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(CountryErrors.NotFound);

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

        if(userResult.IsFailure)
            return Result.Failure<CreatedResponse>(userResult.Error);

        var user = userResult.Value;

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
            return identityResult.ToResult<CreatedResponse>();

        var roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name!);
        if (!roleResult.Succeeded)
            return roleResult.ToResult<CreatedResponse>();

        return user.ToCreatedResponse();
    }

    public async Task<Result> DeletePortalUser(Guid userId)
    {
        var portalUser = await _portalUserRepository.GetPortalUserWithAddress(userId);
        if (portalUser is null)
            return Result.Failure(Error.NotFound);

        portalUser.Deactivate();

        await _portalUserRepository.UpdateAsync(portalUser);

        return Result.Success();
    }

    public async Task<Result<PortalUserResponse>> GetPortalUser(Guid userId, CancellationToken ct = default)
    {
        var portalUser = await _portalUserRepository.GetPortalUserWithAllDataByIdAsync(userId, ct);
        if (portalUser is null)
            return Result.Failure<PortalUserResponse>(Error.NotFound);

        return portalUser.ToPortalUserResponse();
    }

    public async Task<Result<IEnumerable<PortalUserListResponse>>> GetPortalUsersList(CancellationToken ct = default)
    {
        var portalUsers = await _portalUserRepository.GetPortalUsersWithRolesAsync(ct);

        return portalUsers
            .Select(p => p.ToPortalUserListResponse())
            .ToList();
    }

    public Task<Result<UpdatedResponse>> UpdatePortalUser(Guid userId, PortalUserUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
