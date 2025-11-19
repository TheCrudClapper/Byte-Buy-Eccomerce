using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryService
{
    Task<Result<DeliveryResponse>> AddDelivery(DeliveryAddRequest request, CancellationToken ct = default);
    Task<Result<DeliveryResponse>> UpdateDelivery(Guid deliveryId, DeliveryUpdateRequest request, CancellationToken ct = default);
    Task<Result> DeleteDelivery(Guid deliveryId, CancellationToken ct = default);
    Task<Result<DeliveryResponse>> GetDelivery(Guid deliveryId, CancellationToken ct = default);
    Task<Result<IEnumerable<DeliveryResponse>>> GetDeliveries(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList(CancellationToken ct = default);
}
