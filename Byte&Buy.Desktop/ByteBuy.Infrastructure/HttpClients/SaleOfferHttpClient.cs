using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class SaleOfferHttpClient : HttpClientBase, ISaleOfferHttpClient
{
    private const string resource = "saleoffers";
    public SaleOfferHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id)
        => await GetAsync<SaleOfferResponse>($"{resource}/{id}");

    public async Task<Result<IReadOnlyCollection<SaleOfferListResponse>>> GetListAsync()
        => await GetAsync<IReadOnlyCollection<SaleOfferListResponse>>($"{resource}/list");

    public async Task<Result<CreatedResponse>> PostSaleOffer(SaleOfferAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, SaleOfferUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
