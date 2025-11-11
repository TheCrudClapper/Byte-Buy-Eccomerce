using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class CompanyInfoService : ICompanyInfoService
{
    public Task<CompanyInfoResponse> AddCompanyInfo(CompanyInfoAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyInfoResponse> GetCompanyInfo(Guid companyInfoId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyInfoResponse> UpdateCompanyInfo(Guid companyInfoId, CompanyInfoAddRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
