using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDeliveryService : IBaseService
{
    Task<Result<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList();
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectList();
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectList();
    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveries();
    Task<Result<CreatedResponse>> Add(DeliveryAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, DeliveryUpdateRequest request);
    Task<Result<DeliveryResponse>> GetById(Guid id);
    Task<Result<PagedList<DeliveryListResponse>>> GetList(DeliveryListQuery query);
}
