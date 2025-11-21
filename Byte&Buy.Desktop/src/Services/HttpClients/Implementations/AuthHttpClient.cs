using ByteBuy.Services.DTO;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.HttpClients.Implementations;

public class AuthHttpClient(HttpClient client) : HttpClientBase(client), IAuthHttpClient
{
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<TokenResponse>("auth/login-employee", request);

}