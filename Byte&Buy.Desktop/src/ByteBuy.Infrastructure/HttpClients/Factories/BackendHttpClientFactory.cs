using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.ServiceContracts;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Factories;

public sealed class BackendHttpClientFactory(
    IHttpClientFactory httpClientFactory,
    IOptions<ApiEndpointsOptions> options,
    IUserSettingsService settingsService) : IBackendHttpClientFactory
{
    public HttpClient CreateClient(bool requireAuth = true)
    {
        string? userAddress = settingsService.Settings.BackendUrl;
        string baseAddress = string.IsNullOrWhiteSpace(userAddress)
            ? options.Value.DefaultApiUrl
            : userAddress;

        var clientName = requireAuth
            ? "BackendAuthorized"
            : "Backend";

        var httpClient = httpClientFactory.CreateClient(clientName);

        httpClient.BaseAddress = new Uri(
            baseAddress.TrimEnd('/') + "/");

        return httpClient;
    }
}
