using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;
using ByteBuy.Services.ServiceContracts;

namespace ByteBuy.Services.Services;

public class SaleOfferService(ISaleOfferHttpClient httpClient) : ISaleOfferService
{
    public async Task<Result<CreatedResponse>> Add(SaleOfferAddRequest request)
        => await httpClient.PostSaleOffer(request);
    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }
}
