using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryService
    : IBaseCrudService<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>,
      ISelectableService<Guid>
{

    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync(CancellationToken ct = default);
    Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesListAsync(CancellationToken ct = default);
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels();
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes();
}
