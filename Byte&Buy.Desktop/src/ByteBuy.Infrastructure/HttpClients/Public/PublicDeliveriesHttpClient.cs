using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Public;

public class PublicDeliveriesHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory, requireAuth: false), IPublicDeliveriesHttpClient
{
    private readonly string optionsResource = options.Value.DeliveriesOptions;
    private readonly string availableResource = options.Value.DeliveriesAvailable;

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<Guid>>>($"{optionsResource}");

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync()
        => await GetAsync<DeliveryOptionsResponse>($"{availableResource}");
}
