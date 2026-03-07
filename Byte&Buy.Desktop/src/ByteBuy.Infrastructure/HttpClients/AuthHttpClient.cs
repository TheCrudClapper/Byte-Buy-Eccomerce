using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class AuthHttpClient(HttpClient client, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(client, options), IAuthHttpClient
{
    private readonly string resource = options.Value.Auth;

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<TokenResponse>($"{resource}/login-employee", request);

}