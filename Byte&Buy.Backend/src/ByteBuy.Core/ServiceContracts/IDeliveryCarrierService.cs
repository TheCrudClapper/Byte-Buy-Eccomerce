using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.DeliveryCarrier;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryCarrierService
{
    Task<Result<CreatedResponse>> AddDeliveryCarrier(DeliveryCarrierAddRequest request);
    Task<Result<UpdatedResponse>> UpdateDeliveryCarrier(Guid carrierId, DeliveryCarrierUpdateRequest request);
    Task<Result> DeleteDeliveryCarrier(Guid carrierId);
    Task<Result<DeliveryCarrierResponse>> GetDeliveryCarrier(Guid carrierId, CancellationToken ct = default);
    Task<Result<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersList(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct = default);
}
