using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.CompanyInfo;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("companyinfo")]
[Route("api/[controller]")]
[ApiController]
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
        => HandleResult(await _companyInfoService.AddAsync(request));

    [HttpPut]
    //[HasPermission("companyinfo:update")]
    public async Task<ActionResult<UpdatedResponse>> PutCompanyInfo(CompanyInfoUpdateRequest request)
        => HandleResult(await _companyInfoService.UpdateAsync(request));
}
