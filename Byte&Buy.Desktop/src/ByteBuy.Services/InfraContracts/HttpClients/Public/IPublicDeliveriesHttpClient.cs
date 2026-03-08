using ByteBuy.Services.DTO.Delivery;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients.Public;

public interface IPublicDeliveriesHttpClient
{
    Task<Result<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync();
    Task<Result<DeliveryOptionsResponse>> GetAvaliableDeliveriesAsync();
}
