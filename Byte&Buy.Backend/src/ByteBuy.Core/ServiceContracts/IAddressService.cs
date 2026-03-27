using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Address;
using ByteBuy.Core.DTO.Public.AddressValueObj;
using ByteBuy.Core.DTO.Public.Shared;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddShippingAddressAsync(Guid userId, ShippingAddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateShippingAddressAsync(Guid addressId, Guid userId, ShippingAddressUpdateRequest request);
    Task<Result<ShippingAddressResponse>> GetShippingAddressByIdAsync(Guid addressId, CancellationToken ct = default);
    Task<Result<UpdatedResponse>> SetHomeAddressAsync(Guid userId, HomeAddressDto request);
    Task<Result<HomeAddressDto>> GetHomeAddressAsync(Guid userId, CancellationToken ct = default);
    Task<Result<ShippingAddressResponse>> GetShippingAddressAsync(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ShippingAddressListResponse>>> GetShippingAddressesListAsync(Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<ShippingAddressResponse>>> GetUserShippingAddressesAsync(Guid userId, CancellationToken ct = default);
    Task<Result> DeleteShippingAddressAsync(Guid addressId, Guid userId);
    Task<Result<ShippingAddressCheckout>> GetCheckoutAddressAsync(Guid? addressId, Guid userId, CancellationToken ct = default);
}
