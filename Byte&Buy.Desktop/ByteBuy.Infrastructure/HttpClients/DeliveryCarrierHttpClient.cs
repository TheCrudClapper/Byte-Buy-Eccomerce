using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class DeliveryCarrierHttpClient : HttpClientBase, IDeliveryCarrierHttpClient
{
    private const string resource = "deliverycarriers";

    public DeliveryCarrierHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result> DeleteAsync(Guid carrierId)
        => await DeleteAsync(carrierId);

    public async Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid carrierId)
        => await GetAsync<DeliveryCarrierResponse>($"{resource}/{carrierId}");

    public async Task<Result<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersAsync()
        => await GetAsync<IEnumerable<DeliveryCarrierResponse>>($"{resource}/list");

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"{resource}/options");

    public async Task<Result<CreatedResponse>> PostCarrierAsync(DeliveryCarrierAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutCarrierAsync(Guid carrierId, DeliveryCarrierUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{carrierId}", request);
}
