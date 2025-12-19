using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryService
{
    Task<Result<CreatedResponse>> AddDelivery(DeliveryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateDelivery(Guid deliveryId, DeliveryUpdateRequest request);
    Task<Result> DeleteDelivery(Guid deliveryId);
    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveries(CancellationToken ct = default);
    Task<Result<DeliveryResponse>> GetDelivery(Guid deliveryId, CancellationToken ct = default);
    Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesList(CancellationToken ct = default);
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct = default);
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels();
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes();
}
