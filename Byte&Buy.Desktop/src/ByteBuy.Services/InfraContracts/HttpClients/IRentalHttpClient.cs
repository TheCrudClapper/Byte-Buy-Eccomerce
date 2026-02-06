using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.DTO.Rental;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.InfraContracts.HttpClients;

public interface IRentalHttpClient
{
    Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsList();
    Task<Result<RentalLenderResponse>> GetCompanyRental(Guid rentalId);
}
