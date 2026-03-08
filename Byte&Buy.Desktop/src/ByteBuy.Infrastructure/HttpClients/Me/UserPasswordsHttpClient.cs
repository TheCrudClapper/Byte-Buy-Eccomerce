using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Auth;
using ByteBuy.Services.InfraContracts.HttpClients.Me;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Me;

public class UserPasswordsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IUserPasswordHttpClient
{
    private readonly string resource = options.Value.UserMe;

    public async Task<Result> PutPasswordAsync(PasswordChangeRequest request)
        => await PutAsync($"{resource}/password", request);
}
