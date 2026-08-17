namespace ByteBuy.API.Controllers.Public;

[Route("api/countries")]
[ApiController]
public class PublicCountriesController : BaseApiController
{
    private readonly ICountryService _countryService;
    public PublicCountriesController(ICountryService countryService)
        => _countryService = countryService;

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _countryService.GetSelectListAsync(ct));
}
