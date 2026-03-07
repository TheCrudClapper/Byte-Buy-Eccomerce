using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Public;

public class PublicDeliveriesHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options)
{
    private readonly string optionsResource = options.Value.DeliveriesOptions;
    private readonly string availableResource = options.Value.DeliveriesAvailable;

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"{optionsResource}");

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync()
        => await GetAsync<DeliveryOptionsResponse>($"{availableResource}");
}
