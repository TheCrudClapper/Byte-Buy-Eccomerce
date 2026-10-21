using ByteBuy.Core.DTO.Public.CompanyInfo;
using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Public;

[Route("api/public/company/info")]
[ApiController]
[AllowAnonymous]
public class PublicCompanyController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public PublicCompanyController(ICompanyInfoService companyInfoService)
        => _companyInfoService = companyInfoService;

    [HttpGet]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfoAsync(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfoAsync(ct));
}
