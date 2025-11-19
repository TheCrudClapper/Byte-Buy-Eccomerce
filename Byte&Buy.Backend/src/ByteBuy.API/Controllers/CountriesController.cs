using ByteBuy.API.Attributes;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : BaseApiController
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService)
        => _countryService = countryService;

    //[HasPermission("country:write")]
    [HttpPost]
    public async Task<ActionResult> PostCountry(CountryAddRequest request, CancellationToken ct)
        => HandleResult(await _countryService.AddCountry(request, ct));

    [HttpPut("{countryId}")]
    //[HasPermission("country:update")]
    public async Task<ActionResult> PutCountry(Guid countryId, CountryUpdateRequest request, CancellationToken ct)
        => HandleResult(await _countryService.UpdateCountry(countryId, request, ct));

    [HttpDelete("{countryId}")]
    //[HasPermission("country:delete")]
    public async Task<IActionResult> DeleteCountry(Guid countryId, CancellationToken cr)
        => HandleResult(await _countryService.DeleteCountry(countryId, cr));

    [HttpGet("{countryId}")]
    //[HasPermission("country:read")]
    public async Task<ActionResult> GetCountry(Guid countryId, CancellationToken ct)
        => HandleResult(await _countryService.GetCountry(countryId, ct));

    [HttpGet]
    //[HasPermission("country:read:many")]
    public async Task<ActionResult> GetCountries(CancellationToken ct)
        => HandleResult(await _countryService.GetCountries(ct));

    [HttpGet("Options")]
    //[HasPermission("country:selectlist")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _countryService.GetSelectList(ct));
}
