using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Country;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Services.Filtration;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-countries")]
[Route("api/company/countries")]
[ApiController]
public class CompanyCountriesController : CrudControllerBase<Guid, CountryAddRequest, CountryUpdateRequest, CountryResponse>
{
    private readonly ICountryService _countryService;
    public CompanyCountriesController(ICountryService countryService) : base(countryService)
        => _countryService = countryService;

    [HttpGet("list")]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<CountryResponse>>> GetCountriesListAsync([FromQuery] CountryListQuery queryParams, CancellationToken ct)
        => HandleResult(await _countryService.GetCountriesListAsync(queryParams, ct));
}
