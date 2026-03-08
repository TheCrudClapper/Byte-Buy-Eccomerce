using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyDeliveriesHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanyDeliveryHttpClient
{
    private readonly string resource = options.Value.CompanyDeliveries;

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

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsListAsync()
        => await GetAsync<IReadOnlyCollection<SelectListItemResponse<int>>>($"{resource}/channels/list");

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizeListAsync()
         => await GetAsync<IReadOnlyCollection<SelectListItemResponse<int>>>($"{resource}/sizes/list");
}
