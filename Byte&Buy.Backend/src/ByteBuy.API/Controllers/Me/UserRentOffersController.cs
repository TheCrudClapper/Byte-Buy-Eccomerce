using ByteBuy.Core.DTO.Public.Offer.RentOffer;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-rent-offers")]
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
    [HasPermission("{resource}:create:one")]
    public async Task<ActionResult<CreatedResponse>> PostAsync([FromForm] UserRentOfferAddRequest request)
        => HandleResult(await _rentOfferService.AddAsync(CurrentUserId, request));

    [HttpPut("{id:guid}")]
    [HasPermission("{resource}:update:one")]
    public async Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, [FromForm] UserRentOfferUpdateRequest request)
       => HandleResult(await _rentOfferService.UpdateAsync(CurrentUserId, id, request));

    [HttpGet("{id:guid}")]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<UserRentOfferResponse>> GetByIdAsync(Guid id)
        => HandleResult(await _rentOfferService.GetByIdAsync(CurrentUserId, id));

    [HttpDelete("{id:guid}")]
    [HasPermission("{resource}:delete:one")]
    public async Task<IActionResult> DeleteAsync(Guid id)
        => HandleResult(await _rentOfferService.DeleteAsync(CurrentUserId, id));

}
