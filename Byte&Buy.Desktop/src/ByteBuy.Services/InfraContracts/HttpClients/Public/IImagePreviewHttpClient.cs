namespace ByteBuy.Services.InfraContracts.HttpClients.Public;

public interface IImagePreviewHttpClient
{
    Task<byte[]?> GetByteArrayAsync(string relativePath, CancellationToken ct = default);
}
