using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class AddressHttpClient : HttpClientBase, IAddressHttpClient
{
    private const string resource = "users";
    public AddressHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<UpdatedResponse>> PutUserHomeAddressAsync(Guid userId, HomeAddressDto request)
        => await PutAsync<UpdatedResponse>($"{resource}/{userId}/home-address", request);
}
