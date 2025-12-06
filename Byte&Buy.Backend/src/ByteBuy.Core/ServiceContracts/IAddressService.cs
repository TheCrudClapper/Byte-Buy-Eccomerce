using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Address;
using ByteBuy.Core.DTO.PortalUser;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IAddressService
{
    Task<Result<CreatedResponse>> AddAddress(Guid userId, AddressAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAddress(Guid addressId, Guid userId, AddressUpdateRequest request);
    Task<Result<DTO.PortalUser.AddressResponse>> GetAddress(Guid addressId, CancellationToken ct = default);
    Task<Result<DTO.PortalUser.AddressResponse>> GetUserAddress(Guid addressId, Guid userId, CancellationToken ct = default);
    Task<Result<IEnumerable<DTO.PortalUser.AddressResponse>>> GetUserAddresses(Guid userId, CancellationToken ct = default);
    Task<Result> DeleteUserAddress(Guid addressId, Guid userId);
}
