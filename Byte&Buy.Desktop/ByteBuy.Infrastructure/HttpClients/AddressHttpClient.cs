using ByteBuy.Services.DTO.Address;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class AddressHttpClient : HttpClientBase, IAddressHttpClient
{
    private const string resource = "users";
    public AddressHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<CreatedResponse>> PostUserAddressAsync(Guid userId, AddressAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}/{userId}/addresses", request);

    public async Task<Result<UpdatedResponse>> PutUserAddressAsync(Guid userId, Guid addressId, AddressUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{userId}/addresses/{addressId}", request);
}
