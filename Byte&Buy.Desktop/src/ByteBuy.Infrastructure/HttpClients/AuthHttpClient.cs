using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Configuration;

namespace ByteBuy.Infrastructure.HttpClients;

public class AuthHttpClient(HttpClient client, IConfiguration config) 
    : HttpClientBase(client, config), IAuthHttpClient
{
    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<TokenResponse>("auth/login-employee", request);

}