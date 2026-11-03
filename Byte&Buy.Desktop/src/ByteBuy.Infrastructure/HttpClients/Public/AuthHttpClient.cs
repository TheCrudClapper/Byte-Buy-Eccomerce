using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Public;

public class AuthHttpClient(HttpClient client, IOptions<ApiEndpointsOptions> options, IUserSettingsService settingsService)
    : HttpClientBase(client, options, settingsService), IAuthHttpClient
{
    private readonly string resource = options.Value.Auth;

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request)
        => await PostAsync<TokenResponse>($"{resource}/login-employee", request);

}