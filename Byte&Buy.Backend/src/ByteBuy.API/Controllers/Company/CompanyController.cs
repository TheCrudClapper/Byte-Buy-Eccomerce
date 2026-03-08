using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HasPermission("company-info:create:one")]
    public async Task<ActionResult<CreatedResponse>> PostCompanyInfoAsync(CompanyInfoAddRequest request)
        => HandleResult(await _companyInfoService.AddAsync(request));

    [HttpPut]
    [HasPermission("company-info:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutCompanyInfoAsync(CompanyInfoUpdateRequest request)
        => HandleResult(await _companyInfoService.UpdateAsync(request));
}
