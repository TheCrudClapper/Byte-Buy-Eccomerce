using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Country;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("country")]
[Route("api/[controller]")]
[ApiController]
public class CountriesController : CrudControllerBase<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService) : base(countryService)
        => _countryService = countryService;

    [HttpPost]
    //[HasPermission("country:write")]
    public override Task<ActionResult<CreatedResponse>> PostAsync(CountryAddRequest request)
        => base.PostAsync(request);

    [HttpPut("{id}")]
    //[HasPermission("country:update")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, CountryUpdateRequest request) 
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    //[HasPermission("country:delete")]
    public override Task<IActionResult> DeleteAsync(Guid id)
        => base.DeleteAsync(id);

    [HttpGet]
    //[HasPermission("country:read:many")]
    public async Task<ActionResult<IEnumerable<CountryResponse>>> GetCountries(CancellationToken ct)
        => HandleResult(await _countryService.GetCountriesAsync(ct));

    [HttpGet("options")]
    //[HasPermission("country:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _countryService.GetSelectListAsync(ct));
}
