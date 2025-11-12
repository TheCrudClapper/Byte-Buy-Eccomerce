using ByteBuy.Core.DTO.CompanyInfo;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyInfoController : BaseApiController
{
    private readonly ICompanyInfoService _companyInfoService;
    public CompanyInfoController(ICompanyInfoService companyInfoService)
    {
        _companyInfoService = companyInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfo(CancellationToken ct)
        => HandleResult(await _companyInfoService.GetCompanyInfo());

    [HttpPost]
    public async Task<ActionResult<CompanyInfoAddRequest>> PostCompanyInfo(CompanyInfoAddRequest request, CancellationToken ct)
        => HandleResult(await _companyInfoService.AddCompanyInfo(request, ct));

    [HttpPut]
    public async Task<ActionResult<CompanyInfoResponse>> PutCompanyInfo(CompanyInfoUpdateRequest request, CancellationToken ct)
        => HandleResult(await _companyInfoService.UpdateCompanyInfo(request, ct));
}
