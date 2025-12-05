using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<AddressResponse>> GetAddress(Guid addressId, CancellationToken ct = default);
    Task<Result<AddressResponse>> GetUserAddress(Guid userId, Guid addressId, CancellationToken ct = default);
    Task<Result<IEnumerable<AddressResponse>>> GetUserAddresses(Guid userId, CancellationToken ct = default);
    Task<Result<CreatedResponse>> AddAddress(Guid userId, AddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAddress(Guid userId, AddressUpdateRequest request);
}
