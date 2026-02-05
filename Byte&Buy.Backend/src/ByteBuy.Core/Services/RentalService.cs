using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Rental;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.RentalSpecification;
namespace ByteBuy.Core.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICompanyRepository _companyRepository;
    public RentalService(IRentalRepository rentalRepository, ICompanyRepository companyRepository)
    {
        _rentalRepository = rentalRepository;
        _companyRepository = companyRepository;
    }

    public async Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsListAsync(Guid sellerId, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyRentalLenderSpec(companyId);
        return await _rentalRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<IReadOnlyCollection<UserRentalLenderResponse>>> GetSellerRentalsAsync(Guid sellerId, CancellationToken ct = default)
    {
        var spec = new UserRentalLenderSpec(sellerId);
        return await _rentalRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<IReadOnlyCollection<UserRentalBorrowerResponse>>> GetUserRentalsAsync(Guid borrowerId, CancellationToken ct = default)
    {
        var spec = new UserRentalBorrowerSpec(borrowerId);
        return await _rentalRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<UpdatedResponse>> ReturnItemToLenderAsync(Guid borrowerId, Guid rentalId)
    {
        var rental = await _rentalRepository.GetUserRental(borrowerId, rentalId);
        if (rental is null)
            return Result.Failure<UpdatedResponse>(RentalErrors.NotFound);

        rental.ReturnRental();

        await _rentalRepository.UpdateAsync(rental);
        await _rentalRepository.CommitAsync();

        return rental.ToUpdatedResponse();
    }
}
