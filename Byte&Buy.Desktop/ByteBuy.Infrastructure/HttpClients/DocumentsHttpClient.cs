using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.InfraContracts.HttpClients;

namespace ByteBuy.Infrastructure.HttpClients;

public class DocumentsHttpClient : HttpClientBase, IDocumentsHttpClient
{
    private const string resource = "documents";
    public DocumentsHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<byte[]> DownloadOrderDetailsAsync(Guid orderId)
    {
        var response = await Client.GetAsync(
            $"{resource}/order-details/{orderId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
