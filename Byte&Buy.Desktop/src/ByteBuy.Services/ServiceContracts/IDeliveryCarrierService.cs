using ByteBuy.Services.DTO.DeliveryCarrier;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDeliveryCarrierService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<CreatedResponse>> AddAsync(DeliveryCarrierAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryCarrierUpdateRequest request);
    Task<Result<DeliveryCarrierResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<DeliveryCarrierResponse>>> GetListAsync(DeliveryCarriersListQuery query);
}
