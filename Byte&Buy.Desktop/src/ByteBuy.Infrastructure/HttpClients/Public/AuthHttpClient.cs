using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Public;

public class AuthHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory, requireAuth: false), IAuthHttpClient
{
    private readonly string resource = options.Value.Auth;

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<LoginResponse>($"{resource}/login-employee", request);

}