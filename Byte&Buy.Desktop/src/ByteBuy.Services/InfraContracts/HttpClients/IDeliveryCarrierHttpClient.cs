using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IDeliveryCarrierHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersAsync();
    Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid carrierId);
    Task<Result<CreatedResponse>> PostCarrierAsync(DeliveryCarrierAddRequest request);
    Task<Result<UpdatedResponse>> PutCarrierAsync(Guid carrierId, DeliveryCarrierUpdateRequest request);
    Task<Result> DeleteAsync(Guid countryId);
}
