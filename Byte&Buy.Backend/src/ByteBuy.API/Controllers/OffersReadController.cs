using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Offer.Common;
using ByteBuy.Core.DTO.Offer.RentOffer;
using ByteBuy.Core.DTO.Offer.SaleOffer;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[AllowAnonymous]
[Route("api/offers")]
[ApiController]
public class OffersReadController : BaseApiController
{
    private readonly IOfferReadService _offerReadService;
    public OffersReadController(IOfferReadService offerReadService)
    {
        _offerReadService = offerReadService;
    }

    [HttpGet("rent/details/{id:guid}")]
    public async Task<ActionResult<RentOfferDetailsResponse>> GetRentOfferDetails(Guid id, CancellationToken ct)
        => HandleResult(await _offerReadService.GetRentOfferDetails(id, ct));

    [HttpGet("sale/details/{id:guid}")]
    public async Task<ActionResult<SaleOfferDetailsResponse>> GetSaleOfferDetails(Guid id, CancellationToken ct)
        => HandleResult(await _offerReadService.GetSaleOfferDetails(id, ct));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OfferBrowserItemResponse>>> GetOffers(CancellationToken ct)
        => HandleResult(await _offerReadService.BrowseAsync(ct));

}
