using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DocumentService(ICompanyDocumentsHttpClient httpClient) : IDocumentService
{
    public async Task<Result<byte[]>> DownloadOrderDetailsRaportAsync(Guid orderId)
    {
        try
        {
            var file = await httpClient.DownloadOrderDetailsAsync(orderId);
            return Result<byte[]>.Ok(file);
        }
        catch (HttpRequestException)
        {
            return Result<byte[]>.Fail(new Error("You cannot download this document, contact your supervisor."));
        }
        catch (Exception)
        {
            return Result<byte[]>.Fail(new Error("Something went wrong."));
        }
    }
}
