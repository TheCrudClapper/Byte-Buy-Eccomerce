using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.DTO.DeliveryCarrier;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts.Base;

namespace ByteBuy.Core.ServiceContracts;

public interface IDeliveryCarrierService 
    : IBaseCrudService<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest, DeliveryCarrierResponse>,
      ISelectableService<Guid>
{
    Task<Result<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersList(CancellationToken ct = default);
}
