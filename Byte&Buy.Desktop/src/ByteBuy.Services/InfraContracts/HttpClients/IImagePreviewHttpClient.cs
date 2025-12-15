using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;
public interface IImagePreviewHttpClient
{
    Task<byte[]?> GetByteArrayAsync(string relativePath, CancellationToken ct = default);
}
