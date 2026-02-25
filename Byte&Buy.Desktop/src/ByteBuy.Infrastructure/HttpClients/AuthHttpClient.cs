using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class AuthHttpClient(HttpClient client) : HttpClientBase(client), IAuthHttpClient
{
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<TokenResponse>("auth/login-employee", request);

}