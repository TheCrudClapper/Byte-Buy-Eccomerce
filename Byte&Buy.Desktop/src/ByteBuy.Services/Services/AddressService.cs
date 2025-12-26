using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class AddressService(IAddressHttpClient client) : IAddressService
{
    public async Task<Result<CreatedResponse>> AddAsync(Guid userId, AddressAddRequest request)
        => await client.PostUserAddressAsync(userId, request);

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid userId, Guid addressId, AddressUpdateRequest request)
        => await client.PutUserAddressAsync(userId, addressId, request);
}
