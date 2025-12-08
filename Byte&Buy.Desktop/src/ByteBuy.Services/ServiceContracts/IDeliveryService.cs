using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IDeliveryService
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectList();
    Task<Result<CreatedResponse>> Add(DeliveryAddRequest request);
    Task<Result<UpdatedResponse>> Update(Guid id, DeliveryUpdateRequest request);
    Task<Result> DeleteById(Guid id);
    Task<Result<DeliveryResponse>> GetById(Guid id);
    Task<Result<IEnumerable<DeliveryListResponse>>> GetList();
}
