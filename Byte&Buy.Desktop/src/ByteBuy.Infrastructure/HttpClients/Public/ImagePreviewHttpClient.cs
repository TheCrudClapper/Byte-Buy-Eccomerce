using ByteBuy.Services.InfraContracts.HttpClients.Public;


namespace ByteBuy.Infrastructure.HttpClients.Public;


public class ImagePreviewHttpClient(HttpClient client) : IImagePreviewHttpClient
{
    public async Task<byte[]?> GetByteArrayAsync(string relativePath, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return null;

        try
        {
            return await client.GetByteArrayAsync(relativePath, ct);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
