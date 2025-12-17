using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveriesController : BaseApiController
{
    private readonly IDeliveryService _deliveryService;

    public DeliveriesController(IDeliveryService deliveryService)
        => _deliveryService = deliveryService;

    [HttpPost]
    //[HasPermission("delivery:create")]
    public async Task<ActionResult<CreatedResponse>> PostDelivery(DeliveryAddRequest request)
        => HandleResult(await _deliveryService.AddDelivery(request));

    [HttpPut("{deliveryId}")]
    //[HasPermission("delivery:update")]
    public async Task<ActionResult<UpdatedResponse>> PutDelivery(Guid deliveryId, DeliveryUpdateRequest request)
        => HandleResult(await _deliveryService.UpdateDelivery(deliveryId, request));

    [HttpDelete("{deliveryId}")]
    //[HasPermission("delivery:delete")]
    public async Task<IActionResult> DeleteDelivery(Guid deliveryId)
        => HandleResult(await _deliveryService.DeleteDelivery(deliveryId));

    [HttpGet("{deliveryId}")]
    //[HasPermission("delivery:read")]
    public async Task<ActionResult<DeliveryResponse>> GetDelivery(Guid deliveryId, CancellationToken ct)
        => HandleResult(await _deliveryService.GetDelivery(deliveryId, ct));

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<IEnumerable<DeliveryListResponse>>> GetDeliveriesList(CancellationToken ct)
        => HandleResult(await _deliveryService.GetDeliveriesList(ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _deliveryService.GetSelectList(ct));
}
