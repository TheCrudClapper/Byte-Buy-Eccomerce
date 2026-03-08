using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Company;

public interface ICompanyDeliveryHttpClient
{
    Task<Result<PagedList<DeliveryListResponse>>> GetListAsync(DeliveryListQuery query);
    Task<Result<DeliveryResponse>> GetByIdAsync(Guid countryId);
    Task<Result<CreatedResponse>> PostDeliveryAsync(DeliveryAddRequest request);
    Task<Result<UpdatedResponse>> PutDeliveryAsync(Guid deliveryId, DeliveryUpdateRequest request);
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsList();
    Task<Result<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizeList();
    Task<Result> DeleteAsync(Guid deliveryId);
}
