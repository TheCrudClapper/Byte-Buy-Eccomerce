using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Services.ResultTypes;

namespace ByteBuy.Services.ServiceContracts;

public interface IRentalService : IBaseService
{
    Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsList();
}
