using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

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

    public async Task<Result<CreatedResponse>> AddAddress(Guid userId, AddressAddRequest request)
    {
        var country = await _countryRepository.GetByIdAsync(request.CountryId);
        if (country is null)
            return Result.Failure<CreatedResponse>(CountryErrors.NotFound);

        var exist = await _addressRepository.DoesAddressWithLabelExists(userId, request.Label);
        if (exist)
            return Result.Failure<CreatedResponse>(AddressErrors.DuplicateLabel);

        var isDefault = await _addressRepository.DoesUserHaveAdresses(userId);

        var addressResult = Address.Create(
            request.Label,
            request.City,
            request.Street,
            request.HouseNumber,
            request.PostalCity,
            request.PostalCode,
            request.FlatNumber,
            country,
            isDefault,
            _addressValidator
            );

        if (addressResult.IsFailure)
            return Result.Failure<CreatedResponse>(addressResult.Error);

        var address = addressResult.Value;

        await _addressRepository.AddAsync(address);

        return address.ToCreatedResponse();
    }

    public async Task<Result<AddressResponse>> GetUserAddress(Guid userId, Guid addressId, CancellationToken ct = default)
    {
        var address = await _addressRepository.GetUserAddress(userId, addressId, ct);
        if(address is null)
            return Result.Failure<AddressResponse>(Error.NotFound);

        return address.ToAddressResponse();
    }

    public async Task<Result<AddressResponse>> GetAddress(Guid addressId, CancellationToken ct = default)
    {
        var address = await _addressRepository.GetByIdAsync(addressId);
        if (address is null)
            return Result.Failure<AddressResponse>(Error.NotFound);

        return address.ToAddressResponse();
    }

    public Task<Result<UpdatedResponse>> UpdateAddress(Guid userId, AddressUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<AddressResponse>>> GetUserAddresses(Guid userId, CancellationToken ct)
    {
        var addresses = await _addressRepository.GetUserAdressesAsync(userId, ct);
        return addresses
            .Select(a => a.ToAddressResponse())
            .ToList();
    }
}