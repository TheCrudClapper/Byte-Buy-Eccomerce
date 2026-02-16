using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyDeliveriesHttpClient(HttpClient httpClient)
    : HttpClientBase(httpClient), IDeliveryHttpClient
{
    private const string resource = "company/deliveries";

    public async Task<Result<CreatedResponse>> PostDeliveryAsync(DeliveryAddRequest request)
        => await PostAsync<CreatedResponse>($"{resource}", request);

    public async Task<Result<UpdatedResponse>> PutDeliveryAsync(Guid deliveryId, DeliveryUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{deliveryId}", request);

    public async Task<Result> DeleteAsync(Guid deliveryId)
        => await DeleteAsync($"{resource}/{deliveryId}");

    public async Task<Result<DeliveryResponse>> GetByIdAsync(Guid deliveryId)
        => await GetAsync<DeliveryResponse>($"{resource}/{deliveryId}");

    public async Task<Result<PagedList<DeliveryListResponse>>> GetListAsync(DeliveryListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<DeliveryListResponse>>(url);
    }
    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"deliveries/options");

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsList()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<int>>>($"{resource}/channels/list");

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizeList()
         => await GetAsync<IReadOnlyCollection<SelectListItemResponse<int>>>($"{resource}/sizes/list");

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync()
        => await GetAsync<DeliveryOptionsResponse>($"deliveries/available");

}
