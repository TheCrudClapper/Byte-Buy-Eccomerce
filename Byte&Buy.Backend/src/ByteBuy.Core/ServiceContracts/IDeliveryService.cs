using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryService
    : IBaseCrudService<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>,
      ISelectableService<Guid>
{

    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync(CancellationToken ct = default);
    Task<Result<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListAsync(CancellationToken ct = default);
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels();
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes();
    Task<Result<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOffer(Guid offerId, CancellationToken ct = default);
}
