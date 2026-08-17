using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.SaleOffer;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-sale-offers")]
[Route("api/company/sale-offers")]
[ApiController]
public class CompanySaleOffersController : BaseApiController
{
    private readonly ISaleOfferService _saleOfferService;
    public CompanySaleOffersController(ISaleOfferService saleOfferService)
        => _saleOfferService = saleOfferService;

    [HttpPost]
    [HasPermission("{resource}:create:one")]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync(SaleOfferAddRequest request)
        => HandleResult(await _saleOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    [HasPermission("{resource}:update:one")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, SaleOfferUpdateRequest request)
        => HandleResult(await _saleOfferService.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    [HasPermission("{resource}:delete:one")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(id));

    [HttpGet("{id:guid}")]
    [HasPermission("{resource}:read:one")]
    public virtual async Task<ActionResult<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _saleOfferService.GetByIdAsync(id, cancellationToken));

    [HttpGet("list")]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<SaleOfferListResponse>>> GetListAsync([FromQuery] SaleOfferListQuery queryParams, CancellationToken ct)
        => HandleResult(await _saleOfferService.GetListAsync(queryParams, ct));

}
