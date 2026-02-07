using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Infrastructure.HttpClients
{
    internal class CompanyRentalHttpClient : HttpClientBase, IRentalHttpClient
    {
        private const string resource = "rentals";
        public CompanyRentalHttpClient(HttpClient httpClient) : base(httpClient) { }

        public Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsList()
          => GetAsync<IReadOnlyCollection<CompanyRentalLenderResponse>>($"{resource}/company");

        public Task<Result<RentalLenderResponse>> GetCompanyRental(Guid rentalId)
            => GetAsync<RentalLenderResponse>($"{resource}/{rentalId}/company");
    }
}
