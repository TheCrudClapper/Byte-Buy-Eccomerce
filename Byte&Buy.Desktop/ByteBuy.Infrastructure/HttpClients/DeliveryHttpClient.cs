using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class DeliveryHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IDeliveryHttpClient
{
    private const string resource = "deliveries";

    public async Task<Result<CreatedResponse>> PostDeliveryAsync(DeliveryUpdateRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutDeliveryAsync(Guid deliveryId, DeliveryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{deliveryId}", request);

    public async Task<Result> DeleteAsync(Guid deliveryId)
        => await DeleteAsync($"{resource}/{deliveryId}");

    public async Task<Result<DeliveryResponse>> GetByIdAsync(Guid deliveryId)
        => await GetAsync<DeliveryResponse>($"{resource}/{deliveryId}");

    public async Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesList()
        => await GetAsync<IEnumerable<DeliveryListResponse>>($"{resource}/list");

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse>>($"{resource}/options");
}
