using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class UserHttpClient(HttpClient httpClient) 
    : HttpClientBase(httpClient), IUserHttpClient
{
    private const string resource = "users";
    public async Task<Result> PutPasswordAsync(PasswordChangeRequest request)
        => await PutAsync($"{resource}/password", request);
}
