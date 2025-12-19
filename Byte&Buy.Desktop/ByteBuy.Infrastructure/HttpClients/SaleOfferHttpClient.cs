using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class SaleOfferHttpClient : HttpClientBase ,ISaleOfferHttpClient
{
    private const string resource = "saleoffers";
    public SaleOfferHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result<CreatedResponse>> PostSaleOffer(SaleOfferAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);
}
