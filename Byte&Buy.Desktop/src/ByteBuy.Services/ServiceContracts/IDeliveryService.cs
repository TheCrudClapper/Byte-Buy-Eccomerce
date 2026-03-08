using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDeliveryService : IBaseService
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectListAsync();
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectListAsync();
    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync();
    Task<Result<CreatedResponse>> AddAsync(DeliveryAddRequest request);
    Task<Result<UpdatedResponse>> UpdateAsync(Guid id, DeliveryUpdateRequest request);
    Task<Result<DeliveryResponse>> GetByIdAsync(Guid id);
    Task<Result<PagedList<DeliveryListResponse>>> GetListAsync(DeliveryListQuery query);
}
