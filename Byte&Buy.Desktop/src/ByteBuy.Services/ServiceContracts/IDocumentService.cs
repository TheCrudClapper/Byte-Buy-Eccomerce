namespace ByteBuy.Services.ServiceContracts;

public interface IDocumentService
{
    Task<byte[]> DownloadOrderDetailsRaport(Guid orderId);
}
