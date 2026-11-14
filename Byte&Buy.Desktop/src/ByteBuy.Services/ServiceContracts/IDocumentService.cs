using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDocumentService
{
    Task<Result<byte[]>> DownloadOrderDetailsRaportAsync(Guid orderId);
}
