using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class HomeAddressService(IAddressHttpClient client) : IHomeAddressService
{
    public async Task<Result<UpdatedResponse>> SetHomeAddress(Guid userId, HomeAddressDto request)
        => await client.PutUserHomeAddressAsync(userId, request);
}
