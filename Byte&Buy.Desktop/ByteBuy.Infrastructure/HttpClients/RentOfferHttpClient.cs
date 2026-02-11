using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients;

public class RentOfferHttpClient : HttpClientBase, IRentOfferHttpClient
{
    private const string resource = "rentoffers";
    public RentOfferHttpClient(HttpClient httpClient) : base(httpClient) { }

    public async Task<Result> DeleteByIdAsync(Guid id)
         => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<RentOfferResponse>> GetByIdAsync(Guid id)
        => await GetAsync<RentOfferResponse>($"{resource}/{id}");

    public async Task<Result<IReadOnlyCollection<RentOfferListResponse>>> GetListAsync()
        => await GetAsync<IReadOnlyCollection<RentOfferListResponse>>($"{resource}/list");

    public async Task<Result<CreatedResponse>> PostRentOfferAsync(RentOfferAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, RentOfferUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
