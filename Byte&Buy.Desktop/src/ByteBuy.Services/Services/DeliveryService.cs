using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DeliveryService(IDeliveryHttpClient httpClient) : IDeliveryService
{
    public async Task<Result<CreatedResponse>> Add(DeliveryAddRequest request)
        => await httpClient.PostDeliveryAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveries()
        => await httpClient.GetAvaliableDeliveriesAsync();

    public async Task<Result<DeliveryResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectList()
        => await httpClient.GetDeliveryChannelsList();

    public async Task<Result<PagedList<DeliveryListResponse>>> GetList(DeliveryListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectList()
        => await httpClient.GetParcelLockerSizeList();

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid id, DeliveryUpdateRequest request)
        => await httpClient.PutDeliveryAsync(id, request);
}
