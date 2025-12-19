using ByteBuy.Services.DTO.Item;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface ISaleOfferService : IBaseService
{
    Task<Result<CreatedResponse>> Add(SaleOfferAddRequest request);
}
