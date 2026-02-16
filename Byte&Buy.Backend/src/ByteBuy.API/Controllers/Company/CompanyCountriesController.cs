using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Services.Filtration;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;


[Resource("countries")]
[Route("api/company/countries")]
[ApiController]
public class CompanyCountriesController : CrudControllerBase<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>
{
    private readonly ICountryService _countryService;
    public CompanyCountriesController(ICountryService countryService) : base(countryService)
        => _countryService = countryService;

    [HttpGet("list")]
    //[HasPermission("{resource}:read:list")]
    public async Task<ActionResult<PagedList<CountryResponse>>> GetCountriesList([FromQuery] CountryListQuery queryParams, CancellationToken ct)
        => HandleResult(await _countryService.GetCountriesListAsync(queryParams, ct));
}
