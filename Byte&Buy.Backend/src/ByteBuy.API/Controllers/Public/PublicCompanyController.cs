using ByteBuy.Core.DTO.Public.CompanyInfo;

namespace ByteBuy.API.Controllers.Public;

[Route("api/public/company/info")]
[ApiController]
public class PublicCompanyController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public PublicCompanyController(ICompanyInfoService companyInfoService)
        => _companyInfoService = companyInfoService;

    [HttpGet]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfoAsync(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfoAsync(ct));
}
