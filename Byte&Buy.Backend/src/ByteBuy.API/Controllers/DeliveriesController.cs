using ByteBuy.API.Attributes;
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
    public async Task<ActionResult> PostDelivery(DeliveryAddRequest request, CancellationToken ct)
        => HandleResult(await _deliveryService.AddDelivery(request, ct));

    [HttpPut("{deliveryId}")]
    //[HasPermission("delivery:update")]
    public async Task<ActionResult> PutDelivery(Guid deliveryId, DeliveryUpdateRequest request, CancellationToken ct)
        => HandleResult(await _deliveryService.UpdateDelivery(deliveryId, request, ct));

    [HttpDelete("{deliveryId}")]
    //[HasPermission("delivery:delete")]
    public async Task<ActionResult> DeleteDelivery(Guid deliveryId, CancellationToken ct)
        => HandleResult(await _deliveryService.DeleteDelivery(deliveryId, ct));

    [HttpGet("{deliveryId}")]
    //[HasPermission("delivery:read")]
    public async Task<ActionResult> GetDelivery(Guid deliveryId, CancellationToken ct)
        => HandleResult(await _deliveryService.GetDelivery(deliveryId, ct));

    [HttpGet]
    //[HasPermission("delivery:read:many")]
    public async Task<ActionResult> GetDeliveries(CancellationToken ct)
        => HandleResult(await _deliveryService.GetDeliveries(ct));

    [HttpGet("Options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult> GetSelectList(CancellationToken ct)
        => HandleResult(await _deliveryService.GetSelectList(ct));
}
