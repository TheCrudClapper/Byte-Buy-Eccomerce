using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.InfraContracts.HttpClients;
using Microsoft.Extensions.Configuration;

namespace ByteBuy.Infrastructure.HttpClients;

public class DocumentsHttpClient : HttpClientBase, IDocumentsHttpClient
{
    private const string resource = "company/documents";
    public DocumentsHttpClient(HttpClient httpClient, IConfiguration config) 
        : base(httpClient, config) { }

    public async Task<byte[]> DownloadOrderDetailsAsync(Guid orderId)
    {
        var response = await Client.GetAsync(
            $"{resource}/order-details/{orderId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
