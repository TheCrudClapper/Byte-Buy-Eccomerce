using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.RentOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanyRentOffersHttpClient(IBackendHttpClientFactory factory, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(factory), ICompanyRentOfferHttpClient
{
    private readonly string resource = options.Value.CompanyRentOffers;

    public async Task<Result> DeleteByIdAsync(Guid id)
         => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<RentOfferResponse>> GetByIdAsync(Guid id)
        => await GetAsync<RentOfferResponse>($"{resource}/{id}");

    public async Task<Result<PagedList<RentOfferListResponse>>> GetListAsync(RentOfferListQuery query, CancellationToken cancellationToken = default)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<RentOfferListResponse>>(url, cancellationToken);
    }

    public async Task<Result<CreatedResponse>> PostRentOfferAsync(RentOfferAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, RentOfferUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
