using ByteBuy.Core.DTO.Public.Offer.Common;
using ByteBuy.Core.DTO.Public.Offer.RentOffer;
using ByteBuy.Core.DTO.Public.Offer.SaleOffer;
using ByteBuy.Core.Filtration.Offer;

namespace ByteBuy.API.Controllers.Public;

[Route("api/offers")]
[ApiController]
public class PublicOffersReadController : BaseApiController
{
    private readonly IOfferReadService _offerReadService;
    public PublicOffersReadController(IOfferReadService offerReadService)
    {
        _offerReadService = offerReadService;
    }

    [HttpGet("rent/details/{id:guid}")]
    public async Task<ActionResult<RentOfferDetailsResponse>> GetRentOfferDetailsAsync(Guid id, CancellationToken ct)
        => HandleResult(await _offerReadService.GetRentOfferDetailsAsync(id, ct));

    [HttpGet("sale/details/{id:guid}")]
    public async Task<ActionResult<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct)
        => HandleResult(await _offerReadService.GetSaleOfferDetailsAsync(id, ct));

    [HttpGet]
    public async Task<ActionResult<PagedList<OfferBrowserItemResponse>>> GetBrowserOffersAsync([FromQuery] OfferBrowserQuery queryParams, CancellationToken ct)
        => HandleResult(await _offerReadService.BrowseAsync(queryParams, ct));
}
