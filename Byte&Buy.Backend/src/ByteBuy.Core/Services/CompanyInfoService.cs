using ByteBuy.Core.Domain.Companies;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.Domain.Shared.DomainServicesContracts;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CompanyInfoService : ICompanyInfoService
{
    private readonly ICompanyRepository _companyInfoRepository;
    private readonly IAddressValidationService _addressValidator;
    private readonly IUnitOfWork _unitOfWork;
    public CompanyInfoService(ICompanyRepository companyInfo,
        IAddressValidationService addressValidator,
        IUnitOfWork unitOfWork)
    {
        _companyInfoRepository = companyInfo;
        _addressValidator = addressValidator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CreatedResponse>> AddAsync(CompanyInfoAddRequest request)
    {
        if (await _companyInfoRepository.ExistAsync())
            return Result.Failure<CreatedResponse>(CompanyInfoErrors.DuplicateCompanyInfo);

        var createResult = Company.Create(
            request.CompanyName,
            request.TIN,
            request.Email,
            request.PhoneNumber,
            request.Slogan,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.PostalCity,
            request.Address.PostalCode,
            request.Address.City,
            request.Address.Country,
            request.Address.FlatNumber,
            _addressValidator
            );

        if (createResult.IsFailure)
            return Result.Failure<CreatedResponse>(createResult.Error);

        await _companyInfoRepository.AddAsync(createResult.Value);
        await _unitOfWork.SaveChangesAsync();

        return createResult.Value.ToCreatedResponse();
    }

    public async Task<Result<CompanyInfoResponse>> GetCompanyInfoAsync(CancellationToken ct)
    {
        var companyInfo = await _companyInfoRepository.GetAsync(ct);
        return companyInfo is null
            ? Result.Failure<CompanyInfoResponse>(Error.NotFound)
            : companyInfo.ToCompanyInfoResponse();
    }

    public async Task<Result<UpdatedResponse>> UpdateAsync(CompanyInfoUpdateRequest request)
    {
        var companyInfo = await _companyInfoRepository.GetAsync();
        if (companyInfo is null)
            return Result.Failure<UpdatedResponse>(Error.NotFound);

        var updateResult = companyInfo.Update(
            request.CompanyName,
            request.TIN,
            request.Email,
            request.PhoneNumber,
            request.Slogan,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.PostalCity,
            request.Address.PostalCode,
            request.Address.City,
            request.Address.Country,
            request.Address.FlatNumber,
            _addressValidator
            );

        if (updateResult.IsFailure)
            return Result.Failure<UpdatedResponse>(updateResult.Error);

        await _companyInfoRepository.UpdateAsync(companyInfo);
        await _unitOfWork.SaveChangesAsync();

        return companyInfo.ToUpdatedResponse();
    }
}
