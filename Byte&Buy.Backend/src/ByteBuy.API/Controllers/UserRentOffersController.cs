using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/me/rent-offer")]
[ApiController]
public class UserRentOffersController : BaseApiController
{
    private readonly IUserRentOfferService _rentOfferService;
    public UserRentOffersController(IUserRentOfferService rentOfferService)
    {
        _rentOfferService = rentOfferService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedResponse>> PostAsync([FromForm] UserRentOfferAddRequest request)
        => HandleResult(await _rentOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, [FromForm]UserRentOfferUpdateRequest request)
       => HandleResult(await _rentOfferService.UpdateAsync(CurrentUserId, id, request));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserRentOfferResponse>> GetByIdAsync(Guid id)
        => HandleResult(await _rentOfferService.GetByIdAsync(CurrentUserId, id));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _rentOfferService.DeleteAsync(CurrentUserId, id));

}
