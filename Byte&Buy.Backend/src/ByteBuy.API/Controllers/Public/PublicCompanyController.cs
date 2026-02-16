using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Public;

[Route("api/public/company/info")]
[ApiController]
public class PublicCompanyController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public PublicCompanyController(ICompanyInfoService companyInfoService)
        => _companyInfoService = companyInfoService;

    [HttpGet]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfo(ct));
}
