using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients;

public class CompanyDeliveryCarriersHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IDeliveryCarrierHttpClient
{
    private const string resource = "company/delivery-carriers";

    public async Task<Result> DeleteAsync(Guid carrierId)
        => await DeleteAsync($"{resource}/{carrierId}");

    public async Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid carrierId)
        => await GetAsync<DeliveryCarrierResponse>($"{resource}/{carrierId}");

    public async Task<Result<PagedList<DeliveryCarrierResponse>>> GetListAsync(DeliveryCarriersListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<DeliveryCarrierResponse>>(url);
    }

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await GetAsync<IEnumerable<SelectListItemResponse<Guid>>>($"{resource}/options");

    public async Task<Result<CreatedResponse>> PostCarrierAsync(DeliveryCarrierAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutCarrierAsync(Guid carrierId, DeliveryCarrierUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{carrierId}", request);
}
