using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.AddressValueObj;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddUserShippingAddressAsync(Guid userId, ShippingAddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateUserShippingAddressAsync(Guid addressId, Guid userId, ShippingAddressUpdateRequest request);
    Task<Result<ShippingAddressResponse>> GetShippingAddressByIdAsync(Guid addressId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> SetHomeUserAddress(Guid userId, HomeAddressDto request);
    Task<Result<ShippingAddressResponse>> GetUserShippingAddressAsync(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAddressesAsync(Guid userId, CancellationToken ct = default);
    Task<Result> DeleteUserShippingAddressAsync(Guid addressId, Guid userId);
}
