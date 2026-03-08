using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class DeliveryCarrierService(ICompanyDeliveryCarrierHttpClient httpClient) 
    : IDeliveryCarrierService
{
    public async Task<Result<CreatedResponse>> AddAsync(DeliveryCarrierAddRequest request)
        => await httpClient.PostCarrierAsync(request);

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await httpClient.DeleteAsync(id);

    public async Task<Result<PagedList<DeliveryCarrierResponse>>> GetListAsync(DeliveryCarriersListQuery query)
        => await httpClient.GetListAsync(query);

    public async Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid id)
        => await httpClient.GetByIdAsync(id);

    public async Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync()
        => await httpClient.GetSelectListAsync();

    public async Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryCarrierUpdateRequest request)
        => await httpClient.PutCarrierAsync(id, request);
}
