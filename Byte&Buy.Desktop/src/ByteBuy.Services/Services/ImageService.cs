using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class ImageService(IImagePreviewHttpClient htppClient) : IImageService
{
    public async Task<byte[]?> GetImageBytesAsync(string relativePath, CancellationToken ct = default)
    {
        var bytes = await htppClient.GetByteArrayAsync(relativePath, ct);
        return bytes is { Length: > 0 } ? bytes : null;
    }
}
