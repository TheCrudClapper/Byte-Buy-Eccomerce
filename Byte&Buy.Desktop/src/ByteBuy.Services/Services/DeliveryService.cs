using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DeliveryService(ICompanyDeliveryHttpClient httpClient, IPublicDeliveriesHttpClient publicClient) 
    : IDeliveryService
{
    public async Task<Result<CreatedResponse>> AddAsync(DeliveryAddRequest request)
        => await httpClient.PostDeliveryAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync()
        => await publicClient.GetAvaliableDeliveriesAsync();

    public async Task<Result<DeliveryResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectListAsync()
        => await httpClient.GetDeliveryChannelsListAsync();

    public async Task<Result<PagedList<DeliveryListResponse>>> GetListAsync(DeliveryListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectListAsync()
        => await httpClient.GetParcelLockerSizeListAsync();

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await publicClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryUpdateRequest request)
        => await httpClient.PutDeliveryAsync(id, request);
}
