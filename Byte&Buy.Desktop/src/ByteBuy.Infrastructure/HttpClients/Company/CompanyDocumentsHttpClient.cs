using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyDocumentsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyDocumentsHttpClient
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
