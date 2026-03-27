using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryService
    : IBaseCrudService<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>,
      ISelectableService<Guid>
{

    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync(CancellationToken ct = default);
    Task<Result<PagedList<DeliveryListResponse>>> GetDeliveriesListAsync(DeliveryListQuery query, CancellationToken ct = default);
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetDeliveryChannels();
    Result<IReadOnlyCollection<SelectListItemResponse<int>>> GetParcelLockerSizes();
    Task<Result<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOffer(Guid offerId, CancellationToken ct = default);
}
