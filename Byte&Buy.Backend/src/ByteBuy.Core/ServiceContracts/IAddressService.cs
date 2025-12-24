using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddAsync(Guid userId, AddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid addressId, Guid userId, AddressUpdateRequest request);
    Task<Result<AddressResponse>> GetByIdAsync(Guid addressId, CancellationToken ct = default);
    Task<Result<AddressResponse>> GetUserAddressAsync(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<AddressResponse>>> GetUserAddressesAsync(Guid userId, CancellationToken ct = default);
    Task<Result> DeleteUserAddressAsync(Guid addressId, Guid userId);
}
