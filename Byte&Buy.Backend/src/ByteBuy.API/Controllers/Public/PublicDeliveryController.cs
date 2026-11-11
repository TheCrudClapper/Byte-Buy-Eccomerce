using ByteBuy.Core.DTO.Public.Delivery;
using Microsoft.AspNetCore.Authorization;

namespace ByteBuy.API.Controllers.Public;

[Route("api/deliveries")]
[ApiController]
[AllowAnonymous]
public class PublicDeliveryController : BaseApiController
{
    private readonly IDeliveryService _deliveryService;
    public PublicDeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectListAsync(CancellationToken ct)
        => HandleResult(await _deliveryService.GetSelectListAsync(ct));

    [HttpGet("offer/{offerId:guid}")]
    public async Task<ActionResult<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOfferAsync(Guid offerId, CancellationToken ct = default)
        => HandleResult(await _deliveryService.GetDeliveriesListPerOffer(offerId, ct));

    [HttpGet("available")]
    public async Task<ActionResult<DeliveryOptionsResponse>> GetAvailableDeliveriesAsync(CancellationToken ct)
        => HandleResult(await _deliveryService.GetAvaliableDeliveriesAsync(ct));
}
