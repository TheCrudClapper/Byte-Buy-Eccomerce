using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HasPermission("company-sale-offers:create:one")]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync(SaleOfferAddRequest request)
        => HandleResult(await _saleOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    [HasPermission("company-sale-offers:update:one")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, SaleOfferUpdateRequest request)
        => HandleResult(await _saleOfferService.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    [HasPermission("company-sale-offers:delete:one")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(id));

    [HttpGet("{id:guid}")]
    [HasPermission("company-sale-offers:read:one")]
    public virtual async Task<ActionResult<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _saleOfferService.GetByIdAsync(id, cancellationToken));

    [HttpGet("list")]
    [HasPermission("company-sale-offers:read:many")]
    public async Task<ActionResult<PagedList<SaleOfferListResponse>>> GetListAsync([FromQuery] SaleOfferListQuery queryParams, CancellationToken ct)
        => HandleResult(await _saleOfferService.GetListAsync(queryParams, ct));

}
