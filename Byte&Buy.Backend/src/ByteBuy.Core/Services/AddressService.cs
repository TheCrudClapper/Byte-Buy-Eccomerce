using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class AddressService : IAddressService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IAddressReadRepository _addressReadRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IAddressValidationService _addressValidator;
    public AddressService(IAddressReadRepository addressRepository,
        IAddressValidationService addressValidator,
        ICountryRepository countryRepository,
        IPortalUserRepository portalUserRepository)
    {
        _portalUserRepository = portalUserRepository;
        _addressReadRepository = addressRepository;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CreatedResponse>> AddUserShippingAddressAsync(Guid userId, ShippingAddressAddRequest request)
    {
        var spec = new PortalUserWithShippingAddressesSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);
        if (user is null)
            return Result.Failure<CreatedResponse>(Error.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.NotFound);

        var addressResult = user.AddShippingAddress(
            request.Label,
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.FlatNumber,
            country.Id,
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

    public async Task<Result<UpdatedResponse>> UpdateUserShippingAddressAsync(Guid addressId, Guid userId, ShippingAddressUpdateRequest request)
    {
        var spec = new PortalUserWithShippingAddressesSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);
        if (user is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.NotFound);

        var updateResult = user.UpdateShippingAddress(
            addressId,
            request.Label,
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.FlatNumber,
            country.Id,
            request.IsDefault,
            _addressValidator
            );

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();

        var updated = user.ShippingAddresses.Single(a => a.Id == addressId);
        return updated.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> SetHomeUserAddress(Guid userId, HomeAddressDto request)
    {
        var user = await _portalUserRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var homeAddressResult = user.SetHomeAddress(
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.City,
            request.Country,
            request.FlatNumber,
            _addressValidator);

        if (homeAddressResult.IsFailure)
            return Result.Failure<UpdatedResponse>(homeAddressResult.Error);

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();
        return user.ToUpdatedResponse();
    }

    public async Task<Result<HomeAddressDto>> GetUserHomeAddress(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserHomeAddressSpec(userId);
        var address = await _portalUserRepository.GetBySpecAsync(spec);

        return address is null
            ? Result.Failure<HomeAddressDto>(PortalUserErrors.HomeAddressNotSet)
            : address.ToHomeAddressDto();
    }

    public async Task<Result<ShippingAddressResponse>> GetUserShippingAddressAsync(Guid userId, Guid addressId, CancellationToken ct = default)
    {
        var addressDto = await _addressReadRepository.GetBySpecAsync(new UserWithShippingAddresToDtoSpec(userId, addressId), ct);
        if (addressDto is null)
            return Result.Failure<ShippingAddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<IReadOnlyCollection<ShippingAddressListResponse>>> GetShippingAddressesList(Guid userId, CancellationToken ct = default)
    {
        var spec = new UserShippingAddressToList(userId);
        var addressDtoList = await _addressReadRepository.GetListBySpecAsync(spec);

        return addressDtoList is null
            ? Result.Failure<IReadOnlyCollection<ShippingAddressListResponse>>(CommonUserErrors.NotFound)
            : addressDtoList;
    }

    public async Task<Result<ShippingAddressResponse>> GetShippingAddressByIdAsync(Guid addressId, CancellationToken ct = default)
    {
        var addressDto = await _addressReadRepository.GetBySpecAsync(new AddresToDtoSpec(addressId), ct);
        if (addressDto is null)
            return Result.Failure<ShippingAddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAddressesAsync(Guid userId, CancellationToken ct)
        => await _addressReadRepository.GetListBySpecAsync(new UserAddressesToDtoSpec(userId), ct);


    public async Task<Result> DeleteUserShippingAddressAsync(Guid userId, Guid addressId)
    {
        var spec = new PortalUserWithShippingAddressesSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);

        if (user is null)
            return Result.Failure(Error.NotFound);

        var result = user.RemoveShippingAddress(addressId);
        if (result.IsFailure)
            return result;

        await _portalUserRepository.UpdateAsync(user);
        await _portalUserRepository.CommitAsync();
        return Result.Success();
    }
}