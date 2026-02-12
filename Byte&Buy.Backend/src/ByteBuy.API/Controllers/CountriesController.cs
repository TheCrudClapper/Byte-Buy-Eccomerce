using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Services.Filtration;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;


[Resource("countries")]
[Route("api/countries")]
[ApiController]
public class CountriesController : CrudControllerBase<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService) : base(countryService)
        => _countryService = countryService;

    [HttpGet("list")]
    //[HasPermission("{resource}:read:list")]
    public async Task<ActionResult<PagedList<CountryResponse>>> GetCountriesList([FromQuery] CountryListQuery queryParams, CancellationToken ct)
        => HandleResult(await _countryService.GetCountriesListAsync(queryParams, ct));

    [HttpGet("options")]
    //[HasPermission("{resource}:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _countryService.GetSelectListAsync(ct));
}
