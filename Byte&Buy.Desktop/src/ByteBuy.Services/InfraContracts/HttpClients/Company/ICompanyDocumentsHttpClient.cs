namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyDocumentsHttpClient
{
    Task<byte[]> DownloadOrderDetailsAsync(Guid orderId);
}
