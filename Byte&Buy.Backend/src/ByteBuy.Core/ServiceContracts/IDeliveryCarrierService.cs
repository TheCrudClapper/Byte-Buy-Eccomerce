using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.Filtration.DeliveryCarrier;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryCarrierService
    : IBaseCrudService<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest, DeliveryCarrierResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<DeliveryCarrierResponse>>> GetDeliveryCarriersList(DeliveryCarriersListQuery queryParams, CancellationToken ct = default);
}
