using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.RentOffer;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("rentoffers")]
[Route("api/[controller]")]
[ApiController]
public class RentOfferController : BaseApiController
{
    private readonly IRentOfferService _rentOfferService;
    public RentOfferController(IRentOfferService rentOfferService) 
        => _rentOfferService = rentOfferService;

    [HttpPost]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync(RentOfferAddRequest request)
        => HandleResult(await _rentOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, RentOfferUpdateRequest request)
        => HandleResult(await _rentOfferService.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _rentOfferService.DeleteAsync(id));

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<RentOfferResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => HandleResult(await _rentOfferService.GetById(id, cancellationToken));
}
