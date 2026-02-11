namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IDocumentsHttpClient
{
    Task<byte[]> DownloadOrderDetailsAsync(Guid orderId);
}
