using ByteBuy.Core.DTO.Public.CompanyInfo;
using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-info")]
[Route("api/company/info")]
[ApiController]
public class CompanyController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public CompanyController(ICompanyInfoService companyInfoService)
        => _companyInfoService = companyInfoService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfoAsync(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfoAsync());

    [HttpPost]
    [HasPermission("{resource}:create:one")]
    public async Task<ActionResult<CreatedResponse>> PostCompanyInfoAsync(CompanyInfoAddRequest request)
        => HandleResult(await _companyInfoService.AddAsync(request));

    [HttpPut]
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => HandleResult(await _companyInfoService.UpdateAsync(request));
}
