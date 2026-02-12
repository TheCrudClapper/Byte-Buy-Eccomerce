using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Internal.Delivery;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("deliveries")]
[Route("api/[controller]")]
[ApiController]
public class DeliveriesController
    : CrudControllerBase<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>
{
    private readonly IDeliveryService _deliveryService;
    public DeliveriesController(IDeliveryService deliveryService) : base(deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<PagedList<DeliveryListResponse>>> GetDeliveriesList([FromQuery] DeliveryListQuery query, CancellationToken ct)
        => HandleResult(await _deliveryService.GetDeliveriesListAsync(query, ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _deliveryService.GetSelectListAsync(ct));

    [HttpGet("offer/{offerId:guid}")]
    public async Task<ActionResult<IReadOnlyCollection<DeliveryListResponse>>> GetDeliveriesListPerOffer(Guid offerId, CancellationToken ct = default)
        => HandleResult(await _deliveryService.GetDeliveriesListPerOffer(offerId, ct));

    [HttpGet("available")]
    public async Task<ActionResult<DeliveryOptionsResponse>> GetAvailableDeliveries(CancellationToken ct)
        => HandleResult(await _deliveryService.GetAvaliableDeliveriesAsync(ct));

    [HttpGet("sizes/list")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectList()
        => HandleResult(_deliveryService.GetParcelLockerSizes());

    [HttpGet("channels/list")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectList()
        => HandleResult(_deliveryService.GetDeliveryChannels());
}
