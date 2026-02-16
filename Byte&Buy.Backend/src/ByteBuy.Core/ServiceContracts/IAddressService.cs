using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddUserShippingAddressAsync(Guid userId, ShippingAddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateUserShippingAddressAsync(Guid addressId, Guid userId, ShippingAddressUpdateRequest request);
    Task<Result<ShippingAddressResponse>> GetShippingAddressByIdAsync(Guid addressId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> SetHomeUserAddressAsync(Guid userId, HomeAddressDto request);
    Task<Result<HomeAddressDto>> GetUserHomeAddressAsync(Guid userId, CancellationToken ct = default);
    Task<Result<ShippingAddressResponse>> GetUserShippingAddressAsync(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ShippingAddressListResponse>>> GetShippingAddressesList(Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAddressesAsync(Guid userId, CancellationToken ct = default);
    Task<Result> DeleteUserShippingAddressAsync(Guid addressId, Guid userId);
    Task<Result<ShippingAddressCheckout>> GetCheckoutAddress(Guid? addressId, Guid userId, CancellationToken ct = default);
}
