using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.SaleOffer;
using ByteBuy.Services.DTO.Shared;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

public class CompanySaleOfferHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), ICompanySaleOfferHttpClient
{
    private readonly string resource = options.Value.CompanySalesOffers;

    public async Task<Result> DeleteByIdAsync(Guid id)
        => await DeleteAsync($"{resource}/{id}");

    public async Task<Result<SaleOfferResponse>> GetByIdAsync(Guid id)
        => await GetAsync<SaleOfferResponse>($"{resource}/{id}");

    public async Task<Result<PagedList<SaleOfferListResponse>>> GetListAsync(SaleOfferListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}/list", query);
        return await GetAsync<PagedList<SaleOfferListResponse>>(url);
    }

    public async Task<Result<CreatedResponse>> PostSaleOfferAsync(SaleOfferAddRequest request)
        => await PostAsync<CreatedResponse>(resource, request);

    public async Task<Result<UpdatedResponse>> PutRentOfferAsync(Guid id, SaleOfferUpdateRequest request)
        => await PutAsync<UpdatedResponse>($"{resource}/{id}", request);
}
