using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.SaleOffer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("saleoffers")]
[Route("api/[controller]")]
[ApiController]
public class SaleOffersController : BaseApiController
{
    private readonly ISaleOfferService _saleOfferService;
    public SaleOffersController(ISaleOfferService saleOfferService)
        => _saleOfferService = saleOfferService;

    [HttpPost]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync(SaleOfferAddRequest request)
        => HandleResult(await _saleOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, SaleOfferUpdateRequest request)
        => HandleResult(await _saleOfferService.UpdateAsync(id, request));

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(id));

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _saleOfferService.GetByIdAsync(id, cancellationToken));

    [HttpGet("list")]
    public async Task<ActionResult<PagedList<SaleOfferListResponse>>> GetListAsync([FromQuery] SaleOfferListQuery queryParams, CancellationToken ct)
        => HandleResult(await _saleOfferService.GetListAsync(queryParams ,ct));

}
