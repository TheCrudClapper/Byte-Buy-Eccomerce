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

namespace ByteBuy.Core.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IAddressValidationService _addressValidator;
    public AddressService(IAddressRepository addressRepository,
        IAddressValidationService addressValidator,
        ICountryRepository countryRepository)
    {
        _addressRepository = addressRepository;
        _addressValidator = addressValidator;
        _countryRepository = countryRepository;
    }

    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, AddressAddRequest request)
    {
        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(DeliveryCarrierErrors.NotFound);

        var exist = await _addressRepository.DoesAddressWithLabelExists(userId, request.Label);
        if (exist)
            return Result.Failure<CreatedResponse>(AddressErrors.DuplicateLabel);

        if (request.IsDefault)
            await UnsetCurrentDefault(userId);

        var addressResult = Address.Create(
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

        await _addressRepository.AddAsync(address);
        await _addressRepository.CommitAsync();

        return address.ToCreatedResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid addressId, Guid userId, AddressUpdateRequest request)
    {
        var address = await _addressRepository.GetBySpecAsync(new UserAddresSpec(userId, addressId));
        if (address is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<UpdatedResponse>(DeliveryCarrierErrors.NotFound);

        if (address.Label != request.Label)
        {
            var exist = await _addressRepository.DoesAddressWithLabelExists(userId, request.Label);
            if (exist)
                return Result.Failure<UpdatedResponse>(AddressErrors.DuplicateLabel);
        }

        if (address.IsDefault && !request.IsDefault)
            return Result.Failure<UpdatedResponse>(AddressErrors.CannotUnsetCurrentDefault);

        if (!address.IsDefault && request.IsDefault)
            await UnsetCurrentDefault(userId, addressId);

        var updateResult = address.Update(
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

        await _addressRepository.UpdateAsync(address);
        await _addressRepository.CommitAsync();

        return address.ToUpdatedResponse();
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
        var address = await _addressRepository.GetBySpecAsync(new UserAddresSpec(userId, addressId));
        if (address is null)
            return Result.Failure<AddressResponse>(Error.NotFound);

        if (address.IsDefault)
            return Result.Failure<AddressResponse>(AddressErrors.CannotDeleteCurrentDefault);

        address.Deactivate();

        await _addressRepository.UpdateAsync(address);
        await _addressRepository.CommitAsync();
        return Result.Success();
    }

    private async Task UnsetCurrentDefault(Guid userId, Guid? excludeId = null)
    {
        var currentDefault = await _addressRepository
            .GetBySpecAsync(new CurrentDefaultAddressSpec(userId));
        if (currentDefault is null)
            return;

        if (excludeId.HasValue && currentDefault.Id == excludeId)
            return;

        currentDefault.UnmarkAsDefault();
        await _addressRepository.UpdateAsync(currentDefault);
    }

}