using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.InfraContracts.HttpClients;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class DocumentsHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IDocumentsHttpClient
{
    private const string resource = "company/documents";

    public async Task<byte[]> DownloadOrderDetailsAsync(Guid orderId)
    {
        var response = await Client.GetAsync(
            $"{resource}/order-details/{orderId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
