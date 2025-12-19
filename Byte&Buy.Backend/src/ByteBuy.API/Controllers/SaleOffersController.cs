using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

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

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, SaleOfferUpdateRequest request)
        => HandleResult(await _saleOfferService.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _saleOfferService.DeleteAsync(id));

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<SaleOfferResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _saleOfferService.GetById(id, cancellationToken));

}
