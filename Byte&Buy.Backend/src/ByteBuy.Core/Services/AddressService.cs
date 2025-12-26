using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class AddressService : IAddressService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IAddressReadRepository _addressRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IAddressValidationService _addressValidator;
    public AddressService(IAddressReadRepository addressRepository,
        IAddressValidationService addressValidator,
        ICountryRepository countryRepository,
        IPortalUserRepository portalUserRepository)
    {
        _portalUserRepository = portalUserRepository;
        _addressRepository = addressRepository;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, AddressAddRequest request)
    {
        var spec = new PortalUserWithAddressSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);
        if (user is null)
            return Result.Failure<CreatedResponse>(Error.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.NotFound);

        var addressResult = user.AddAddress(
            request.Label,
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.FlatNumber,
            country,
            request.IsDefault,
            _addressValidator
            );

        if (addressResult.IsFailure)
            return Result.Failure<CreatedResponse>(addressResult.Error);

        var address = addressResult.Value;

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();

        return address.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid addressId, Guid userId, AddressUpdateRequest request)
    {
        var spec = new PortalUserWithAddressSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);
        if (user is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.NotFound);

        var updateResult = user.UpdateAddress(
            addressId,
            request.Label,
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.FlatNumber,
            country,
            request.IsDefault,
            _addressValidator
            );

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();

        return user.ToUpdatedResponse();
    }

    public async Task<Result<AddressResponse>> GetUserAddressAsync(Guid userId, Guid addressId, CancellationToken ct = default)
    {
        var addressDto = await _addressRepository.GetBySpecAsync(new UserAddresToDtoSpec(userId, addressId), ct);
        if (addressDto is null)
            return Result.Failure<AddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<AddressResponse>> GetByIdAsync(Guid addressId, CancellationToken ct = default)
    {
        var addressDto = await _addressRepository.GetBySpecAsync(new AddresToDtoSpec(addressId), ct);
        if (addressDto is null)
            return Result.Failure<AddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<IReadOnlyCollection<AddressResponse>>> GetUserAddressesAsync(Guid userId, CancellationToken ct)
        => await _addressRepository.GetListBySpecAsync(new UserAddressesToDtoSpec(userId), ct);


    public async Task<Result> DeleteUserAddressAsync(Guid userId, Guid addressId)
    {
        var spec = new PortalUserWithAddressSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);

        if (user is null)
            return Result.Failure(Error.NotFound);

        var result = user.RemoveAddress(addressId);
        if (result.IsFailure)
            return result;

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();
        return Result.Success();
    }
}