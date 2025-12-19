using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface ISaleOfferHttpClient
{
    Task<Result<CreatedResponse>> PostSaleOffer(SaleOfferAddRequest request);
}
