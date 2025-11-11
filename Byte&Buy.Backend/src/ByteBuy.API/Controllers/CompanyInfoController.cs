using ByteBuy.Core.DTO.CompanyInfo;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyInfoController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<CompanyInfoResponse>> GetCompanyInfo()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<CompanyInfoAddRequest>> PostCompanyInfo()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<CompanyInfoResponse>> PutCompanyInfo()
    {
        throw new NotImplementedException();
    }
}
