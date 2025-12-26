using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IAddressHttpClient
{
    Task<Result<CreatedResponse>> PostUserAddressAsync(Guid userId, AddressAddRequest request);
    Task<Result<UpdatedResponse>> PutUserAddressAsync(Guid userId, Guid addressId, AddressUpdateRequest request);
}
