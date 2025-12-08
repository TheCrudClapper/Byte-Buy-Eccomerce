using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IDeliveryHttpClient
{
    Task<Result<IEnumerable<SelectListItemResponse>>> GetSelectListAsync();
    Task<Result<IEnumerable<DeliveryListResponse>>> GetDeliveriesList();
    Task<Result<DeliveryResponse>> GetByIdAsync(Guid countryId);
    Task<Result<CreatedResponse>> PostDeliveryAsync(DeliveryUpdateRequest request);
    Task<Result<UpdatedResponse>> PutDeliveryAsync(Guid deliveryId, DeliveryUpdateRequest request);
    Task<Result> DeleteAsync(Guid deliveryId);
}
