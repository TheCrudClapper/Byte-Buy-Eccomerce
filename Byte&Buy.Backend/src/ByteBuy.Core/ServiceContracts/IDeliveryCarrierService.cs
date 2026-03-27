using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.Filtration.DeliveryCarrier;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryCarrierService
    : IBaseCrudService<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest, DeliveryCarrierResponse>,
      ISelectableService<Guid>
{
    Task<Result<PagedList<DeliveryCarrierResponse>>> GetDeliveryCarriersListAsync(DeliveryCarriersListQuery queryParams, CancellationToken ct = default);
}
