using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.Mappings;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CompanyInfoService : ICompanyInfoService
{
    private readonly ICompanyInfoRepository _companyInfoRepository;
    public CompanyInfoService(ICompanyInfoRepository companyInfo)
    {
        _companyInfoRepository = companyInfo;
    }
    public async Task<Result<CompanyInfoResponse>> AddCompanyInfo(CompanyInfoAddRequest request)
    {
        if (await _companyInfoRepository.ExistAsync())
            return Result.Failure<CompanyInfoResponse>(CompanyInfoErrors.MultipleCompanyInfos);

        var createResult = CompanyInfo.Create(
            request.CompanyName,
            request.TIN,
            request.Email,
            request.PhoneNumber,
            request.Slogan,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.PostalCode,
            request.Address.City,
            request.Address.Country,
            request.Address.FlatNumber
            );

        if(createResult.IsFailure)
            return Result.Failure<CompanyInfoResponse>(createResult.Error);

        await _companyInfoRepository.AddAsync(createResult.Value);

        return createResult.Value.ToCompanyInfoResponse();
    }

    public async Task<Result<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct)
    {
        var companyInfo = await _companyInfoRepository.GetAsync(ct);
        return companyInfo is null
            ? Result.Failure<CompanyInfoResponse>(Error.NotFound)
            : companyInfo.ToCompanyInfoResponse();
    }

    public async Task<Result<CompanyInfoResponse>> UpdateCompanyInfo(CompanyInfoUpdateRequest request)
    {
        var companyInfo = await _companyInfoRepository.GetAsync();
        if (companyInfo is null)
            return Result.Failure<CompanyInfoResponse>(Error.NotFound);

        var updateResult = companyInfo.Update(
            request.CompanyName,
            request.TIN,
            request.Email,
            request.PhoneNumber,
            request.Slogan,
            request.Address.Street,
            request.Address.HouseNumber,
            request.Address.PostalCode,
            request.Address.City,
            request.Address.Country,
            request.Address.FlatNumber);

        if (updateResult.IsFailure)
            return Result.Failure<CompanyInfoResponse>(updateResult.Error);

        await _companyInfoRepository.UpdateAsync(companyInfo);
        return companyInfo.ToCompanyInfoResponse();
    }
}
