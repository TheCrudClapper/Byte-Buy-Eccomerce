using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ServiceContracts;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyDocumentsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options, IUserSettingsService settingsService)
    : HttpClientBase(httpClient, options, settingsService), ICompanyDocumentsHttpClient
{
    private readonly string resource = options.Value.CompanyDocuments;

    public async Task<byte[]> DownloadOrderDetailsAsync(Guid orderId)
    {
        var response = await Client.GetAsync(
            $"{resource}/order-details/{orderId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
