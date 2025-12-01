using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class UserHttpClient(HttpClient httpClient) : HttpClientBase(httpClient), IUserHttpClient
{
    public async Task<Result> ChangePasswordAsync(PasswordChangeRequest request)
        => await PutAsync("users/password", request);
}
