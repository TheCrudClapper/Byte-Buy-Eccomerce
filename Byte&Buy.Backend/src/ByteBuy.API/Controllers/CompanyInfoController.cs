using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CompanyInfoController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public CompanyInfoController(ICompanyInfoService companyInfoService)
        => _companyInfoService = companyInfoService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfo());

    [HttpPost]
    //[HasPermission("companyinfo:create")]
    public async Task<ActionResult<CreatedResponse>> PostCompanyInfo(CompanyInfoAddRequest request)
        => HandleResult(await _companyInfoService.AddCompanyInfo(request));

    [HttpPut]
    //[HasPermission("companyinfo:update")]
    public async Task<ActionResult<UpdatedResponse>> PutCompanyInfo(CompanyInfoUpdateRequest request)
        => HandleResult(await _companyInfoService.UpdateCompanyInfo(request));
}
