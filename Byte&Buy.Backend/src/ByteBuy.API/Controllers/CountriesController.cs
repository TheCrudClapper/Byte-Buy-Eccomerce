using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("countries")]
[Route("api/[controller]")]
[ApiController]
public class CountriesController : CrudControllerBase<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService) : base(countryService)
        => _countryService = countryService;

    [HttpGet]
    //[HasPermission("country:read:many")]
    public async Task<ActionResult<IEnumerable<CountryResponse>>> GetCountries(CancellationToken ct)
        => HandleResult(await _countryService.GetCountriesAsync(ct));

    [HttpGet("options")]
    //[HasPermission("country:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _countryService.GetSelectListAsync(ct));
}
