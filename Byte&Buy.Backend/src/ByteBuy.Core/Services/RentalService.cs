using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
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
    private readonly IUnitOfWork _unitOfWork;

    public RentalService(IRentalRepository rentalRepository,
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork)
    {
        _rentalRepository = rentalRepository;
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RentalLenderResponse>> GetCompanyRentalAsync(Guid rentalId, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyRentalLenderSpec(companyId, rentalId);
        var dto = await _rentalRepository.GetBySpecAsync(spec, ct);

        return dto is null
            ? Result.Failure<RentalLenderResponse>(RentalErrors.NotFound)
            : dto;
    }

    public async Task<Result<IReadOnlyCollection<CompanyRentalLenderResponse>>> GetCompanyRentalsListAsync(Guid sellerId, CancellationToken ct = default)
    {
        var companyId = await _companyRepository.GetCompanyId(ct);

        var spec = new CompanyRentalListLenderSpec(companyId);
        return await _rentalRepository.GetListBySpecAsync(spec, ct);
    }

    public async Task<Result<IReadOnlyCollection<RentalLenderResponse>>> GetSellerRentalsAsync(Guid sellerId, CancellationToken ct = default)
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

        var returnResult = rental.ReturnRental();
        if (returnResult.IsFailure)
            return Result.Failure<UpdatedResponse>(returnResult.Error);

        await _rentalRepository.UpdateAsync(rental);
        await _unitOfWork.SaveChangesAsync();

        return rental.ToUpdatedResponse();
    }
}
