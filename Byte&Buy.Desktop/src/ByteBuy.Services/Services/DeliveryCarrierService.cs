using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DeliveryCarrierService(IDeliveryCarrierHttpClient httpClient) : IDeliveryCarrierService
{
    public async Task<Result<CreatedResponse>> Add(DeliveryCarrierAddRequest request)
        => await httpClient.PostCarrierAsync(request);

    public async Task<Result> DeleteById(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<PagedList<DeliveryCarrierResponse>>> GetList(DeliveryCarriersListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<DeliveryCarrierResponse>> GetById(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> Update(Guid id, DeliveryCarrierUpdateRequest request)
        => await httpClient.PutCarrierAsync(id, request);
}
