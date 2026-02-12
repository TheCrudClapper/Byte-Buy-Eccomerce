using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDeliveryCarrierService : IBaseService
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(DeliveryCarrierAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, DeliveryCarrierUpdateRequest request);
    Task<Result<DeliveryCarrierResponse>> GetById(Guid id);
    Task<Result<PagedList<DeliveryCarrierResponse>>> GetList(DeliveryCarriersListQuery query);
}
