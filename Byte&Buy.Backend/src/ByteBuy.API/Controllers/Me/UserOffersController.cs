using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.Filtration.Offer;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me/offers")]
[ApiController]
public class UserOffersController : BaseApiController
{
    private readonly IOfferReadService _offerReadService;
    public UserOffersController(IOfferReadService offerReadService)
        => _offerReadService = offerReadService;
    
    [HttpGet]
    public async Task<ActionResult<PagedList<UserPanelOfferResponse>>> GetUserOffers([FromQuery] UserOffersQuery queryParams, CancellationToken ct)
       => HandleResult(await _offerReadService.GetUserPanelOffers(queryParams, CurrentUserId, ct));
}
