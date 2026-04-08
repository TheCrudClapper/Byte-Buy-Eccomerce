using ByteBuy.Core.Domain.DeliveryCarriers.Errors;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.Domain.Shared.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.Domain.Users.Errors;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.AddressSpecifications;
using static ByteBuy.Core.Specification.PortalUserSpecifications;

namespace ByteBuy.Core.Services;

public class AddressService : IAddressService
{
    private readonly IPortalUserRepository _portalUserRepository;
    private readonly IAddressReadRepository _addressReadRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAddressValidationService _addressValidator;
    public AddressService(IAddressReadRepository addressRepository,
        IAddressValidationService addressValidator,
        ICountryRepository countryRepository,
        IPortalUserRepository portalUserRepository,
        IUnitOfWork unitOfWork)
    {
        _portalUserRepository = portalUserRepository;
        _addressReadRepository = addressRepository;
        _unitOfWork = unitOfWork;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CreatedResponse>> AddShippingAddressAsync(Guid userId, ShippingAddressAddRequest request)
    {
        var spec = new PortalUserAggregateSpec(userId);
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
        await _unitOfWork.SaveChangesAsync();

        return address.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateShippingAddressAsync(Guid addressId, Guid userId, ShippingAddressUpdateRequest request)
    {
        var spec = new PortalUserAggregateSpec(userId);
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
        await _unitOfWork.SaveChangesAsync();

        var updated = user.ShippingAddresses.Single(a => a.Id == addressId);
        return updated.ToUpdatedResponse();
    }

    public async Task<Result<UpdatedResponse>> SetHomeAddressAsync(Guid userId, HomeAddressDto request)
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
        await _unitOfWork.SaveChangesAsync();
        return user.ToUpdatedResponse();
    }

    public async Task<Result<HomeAddressDto>> GetHomeAddressAsync(Guid userId, CancellationToken ct)
    {
        var spec = new UserHomeAddressSpec(userId);
        var address = await _portalUserRepository.GetBySpecAsync(spec, ct);

        return address is null
            ? Result.Failure<HomeAddressDto>(PortalUserErrors.HomeAddressNotSet)
            : address.ToHomeAddressDto();
    }

    public async Task<Result<ShippingAddressResponse>> GetShippingAddressAsync(Guid userId, Guid addressId, CancellationToken ct)
    {
        var addressDto = await _addressReadRepository.GetBySpecAsync(new UserAndShippingAddressResponseSpec(userId, addressId), ct);
        if (addressDto is null)
            return Result.Failure<ShippingAddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<IReadOnlyCollection<ShippingAddressListResponse>>> GetShippingAddressesListAsync(Guid userId, CancellationToken ct)
    {
        var spec = new UserShippingAddressListResponseSpec(userId);
        var addressDtoList = await _addressReadRepository.GetListBySpecAsync(spec);

        return addressDtoList is null
            ? Result.Failure<IReadOnlyCollection<ShippingAddressListResponse>>(CommonUserErrors.NotFound)
            : addressDtoList;
    }

    public async Task<Result<ShippingAddressResponse>> GetShippingAddressByIdAsync(Guid addressId, CancellationToken ct)
    {
        var addressDto = await _addressReadRepository.GetBySpecAsync(new ShippingAddressResponseSpec(addressId), ct);
        if (addressDto is null)
            return Result.Failure<ShippingAddressResponse>(Error.NotFound);

        return addressDto;
    }

    public async Task<Result<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAddressesAsync(Guid userId, CancellationToken ct)
        => await _addressReadRepository.GetListBySpecAsync(new UserShippingAddressResponseSpec(userId), ct);

    public async Task<Result> DeleteShippingAddressAsync(Guid addressId, Guid userId)
    {
        var spec = new PortalUserAggregateSpec(userId);
        var user = await _portalUserRepository.GetBySpecAsync(spec);

        if (user is null)
            return Result.Failure(Error.NotFound);

        var result = user.RemoveShippingAddress(addressId);
        if (result.IsFailure)
            return result;

        await _portalUserRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<ShippingAddressCheckout>> GetCheckoutAddressAsync(Guid? addressId, Guid userId, CancellationToken ct)
    {
        var spec = new UserShippingAddressCheckoutSpec(userId, addressId);
        var address = await _addressReadRepository.GetBySpecAsync(spec, ct);

        return address is null
            ? Result.Failure<ShippingAddressCheckout>(AddressErrors.NoDefaultAddress)
            : address;
    }

    public async Task<Result<OfferAddressResponse?>> GetHomeAddressForOfferAsync(Guid userId, CancellationToken ct = default)
    {
        var spec = new HomeAddressForOfferSpec(userId);
        var addressDto = await _portalUserRepository.GetBySpecAsync(spec, ct);

        return addressDto is null
           ? Result.Failure<OfferAddressResponse?>(PortalUserErrors.HomeAddressNotSet)
           : addressDto;
    }
}