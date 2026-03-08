namespace ByteBuy.Services.ServiceContracts;

public interface IDocumentService
{
    Task<byte[]> DownloadOrderDetailsRaportAsync(Guid orderId);
}
