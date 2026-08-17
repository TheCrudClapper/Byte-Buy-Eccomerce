using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.Filtration.Offer;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-offers")]
[Route("api/me/offers")]
[ApiController]
public class UserOffersController : BaseApiController
{
    private readonly IOfferReadService _offerReadService;
    public UserOffersController(IOfferReadService offerReadService)
        => _offerReadService = offerReadService;

    [HttpGet]
    [HasPermission("{resource}:read:many")]
    public async Task<ActionResult<PagedList<UserPanelOfferResponse>>> GetUserOffersAsync([FromQuery] UserOffersQuery queryParams, CancellationToken ct)
       => HandleResult(await _offerReadService.GetUserPanelOffersAsync(queryParams, CurrentUserId, ct));
}
