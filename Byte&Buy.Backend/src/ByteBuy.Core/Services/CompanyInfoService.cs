using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CompanyInfoService : ICompanyInfoService
{
    private readonly ICompanyInfoRepository _companyInfoRepository;
    private readonly IAddressValidationService _addressValidator;
    public CompanyInfoService(ICompanyInfoRepository companyInfo,
        IAddressValidationService addressValidator)
    {
        _companyInfoRepository = companyInfo;
        _addressValidator = addressValidator;
    }
    public async Task<Result<CreatedResponse>> AddAsync(CompanyInfoAddRequest request)
    {
        if (await _companyInfoRepository.ExistAsync())
            return Result.Failure<CreatedResponse>(CompanyInfoErrors.DuplicateCompanyInfo);

        var createResult = CompanyInfo.Create(
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
        await _companyInfoRepository.CommitAsync();

        return createResult.Value.ToCreatedResponse();
    }

    public async Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct)
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
        await _companyInfoRepository.CommitAsync();

        return companyInfo.ToUpdatedResponse();
    }
}
