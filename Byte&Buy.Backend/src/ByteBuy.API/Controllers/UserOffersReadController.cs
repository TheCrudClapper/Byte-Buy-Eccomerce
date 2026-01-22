using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/me/offers")]
[ApiController]
public class UserOffersReadController :  BaseApiController
{
    private readonly IOfferReadService _offerReadService;
    public UserOffersReadController(IOfferReadService offerReadService)
    {
        _offerReadService = offerReadService;
    }

    [HttpGet()]
    public async Task<ActionResult<IReadOnlyCollection<UserPanelOfferResponse>>> GetUserOffers(CancellationToken ct)
        => HandleResult(await _offerReadService.GetUserPanelOffers(CurrentUserId, ct));
}
