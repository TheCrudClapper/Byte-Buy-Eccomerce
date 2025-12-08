using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DeliveryService(IDeliveryHttpClient httpClient) : IDeliveryService
{
    public async Task<Result<CreatedResponse>> Add(DeliveryAddRequest request)
        => await httpClient.PostDeliveryAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<DeliveryResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IEnumerable<DeliveryListResponse>>> GetList()
        => await httpClient.GetListAsync();

    public async Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid id, DeliveryUpdateRequest request)
        => await httpClient.PutDeliveryAsync(id, request);
}
