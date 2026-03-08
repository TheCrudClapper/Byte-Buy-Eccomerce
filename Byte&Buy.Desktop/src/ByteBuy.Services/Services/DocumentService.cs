using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DocumentService(ICompanyDocumentsHttpClient httpClient) : IDocumentService
{
    public async Task<byte[]> DownloadOrderDetailsRaportAsync(Guid orderId)
        => await httpClient.DownloadOrderDetailsAsync(orderId);
}
