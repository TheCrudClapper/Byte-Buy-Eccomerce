using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using Microsoft.Extensions.Options;
namespace ByteBuy.Infrastructure.HttpClients.Public;

public class ImagePreviewHttpClient : IImagePreviewHttpClient
{
    private readonly IBackendHttpClientFactory _clientFactory;
    private readonly IOptions<ApiEndpointsOptions> _options;
    public ImagePreviewHttpClient(IBackendHttpClientFactory clientFactory, IOptions<ApiEndpointsOptions> options)
    {
        _clientFactory = clientFactory;
        _options = options;
    }

    public async Task<byte[]?> GetByteArrayAsync(string relativePath, CancellationToken ct = default)
    {
        var client = _clientFactory.CreateClient();
        string resourceUrl = $"{_options.Value.Images}/{relativePath}";
        if (string.IsNullOrWhiteSpace(relativePath))
            return null;

        try
        {
            return await client.GetByteArrayAsync(resourceUrl, ct);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
