using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Public;

[Route("api/deliveries")]
[ApiController]
public class PublicDeliveryController : BaseApiController
{
    private readonly IDeliveryService _deliveryService;
    public PublicDeliveryController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpGet("options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _deliveryService.GetSelectListAsync(ct));

    [HttpGet("offer/{offerId:guid}")]
    public async Task<ActionResult<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOffer(Guid offerId, CancellationToken ct = default)
        => HandleResult(await _deliveryService.GetDeliveriesListPerOffer(offerId, ct));

    [HttpGet("available")]
    public async Task<ActionResult<DeliveryOptionsResponse>> GetAvailableDeliveries(CancellationToken ct)
        => HandleResult(await _deliveryService.GetAvaliableDeliveriesAsync(ct));
}
