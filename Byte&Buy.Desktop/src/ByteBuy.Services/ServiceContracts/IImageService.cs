namespace ByteBuy.Services.ServiceContracts;

public interface IImageService
{
    Task<byte[]?> GetImageBytesAsync(string relativePath, CancellationToken ct = default);
}
