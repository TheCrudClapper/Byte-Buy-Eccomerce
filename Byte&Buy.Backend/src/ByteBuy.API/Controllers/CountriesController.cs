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
    {
        _countryService = countryService;
    }

    //[HasPermission("country:write")]
    [HttpPost]
    public async Task<ActionResult> PostCountry(CountryAddRequest request, CancellationToken cancellationToken)
        => HandleResult(await _countryService.AddCountry(request, cancellationToken));

    [HttpPut("{countryId}")]
    //[HasPermission("country:update")]
    public async Task<ActionResult> PutCountry(Guid countryId, CountryUpdateRequest request, CancellationToken cancellationToken)
        => HandleResult(await _countryService.UpdateCountry(countryId, request, cancellationToken));

    [HttpDelete("{countryId}")]
    //[HasPermission("country:delete")]
    public async Task<IActionResult> DeleteCountry(Guid countryId, CancellationToken cancellationToken)
        => HandleResult(await _countryService.DeleteCountry(countryId, cancellationToken));

    [HttpGet("{countryId}")]
    //[HasPermission("country:read")]
    public async Task<ActionResult> GetCountry(Guid countryId, CancellationToken cancellationToken)
        => HandleResult(await _countryService.GetCountry(countryId, cancellationToken));

    [HttpGet]
    //[HasPermission("country:read:many")]
    public async Task<ActionResult> GetCountries(CancellationToken cancellationToken)
        => HandleResult(await _countryService.GetCountries(cancellationToken));

    [HttpGet("Options")]
    //[HasPermission("country:selectlist")]
    public async Task<ActionResult> GetSelectList(CancellationToken cancellationToken)
        => HandleResult(await _countryService.GetSelectList(cancellationToken));
}
