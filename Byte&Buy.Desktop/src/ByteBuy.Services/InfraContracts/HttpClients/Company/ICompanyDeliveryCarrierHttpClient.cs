using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyDeliveryCarrierHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<PagedList<DeliveryCarrierResponse>>> GetListAsync(DeliveryCarriersListQuery query);
    Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid carrierId);
    Task<Result<CreatedResponse>> PostCarrierAsync(DeliveryCarrierAddRequest request);
    Task<Result<UpdatedResponse>> PutCarrierAsync(Guid carrierId, DeliveryCarrierUpdateRequest request);
    Task<Result> DeleteAsync(Guid countryId);
}
