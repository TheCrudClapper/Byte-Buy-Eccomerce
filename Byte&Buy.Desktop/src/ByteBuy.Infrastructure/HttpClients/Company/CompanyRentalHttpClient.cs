using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Infrastructure.Helpers;
using ByteBuy.Infrastructure.HttpClients.Base;
using ByteBuy.Infrastructure.Options;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.Filtration;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.Pagination;
using ByteBuy.Services.ResultTypes;
using Microsoft.Extensions.Options;

namespace ByteBuy.Infrastructure.HttpClients.Company;

internal class CompanyRentalHttpClient(HttpClient httpClient, IOptions<ApiEndpointsOptions> options)
    : HttpClientBase(httpClient, options), IRentalHttpClient
{
    private readonly string resource = options.Value.CompanyRentals;

    public Task<Result<PagedList<CompanyRentalLenderListResponse>>> GetCompanyRentalsList(RentalListQuery query)
    {
        var url = QueryStringHelper.ToQueryString($"{resource}", query);
        return GetAsync<PagedList<CompanyRentalLenderListResponse>>(url);
    }

    public Task<Result<RentalLenderResponse>> GetCompanyRental(Guid rentalId)
        => GetAsync<RentalLenderResponse>($"{resource}/{rentalId}");
}
