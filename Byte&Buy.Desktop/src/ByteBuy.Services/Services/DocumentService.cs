using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DocumentService(IDocumentsHttpClient httpClient) : IDocumentService
{
    public async Task<byte[]> DownloadOrderDetailsRaport(Guid orderId)
        => await httpClient.DownloadOrderDetailsAsync(orderId);
}
