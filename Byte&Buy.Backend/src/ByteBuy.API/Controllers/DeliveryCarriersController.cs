using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.DTO.DeliveryCarrier;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryCarriersController : BaseApiController
{
    private readonly IDeliveryCarrierService _carrierService;
    public DeliveryCarriersController(IDeliveryCarrierService carrierService)
         => _carrierService = carrierService;

    [HttpPost]
    //[HasPermission("delivery:create")]
    public async Task<ActionResult<CreatedResponse>> PostDeliveryCarrier(DeliveryCarrierAddRequest request)
       => HandleResult(await _carrierService.AddDeliveryCarrier(request));

    [HttpPut("{carrierId}")]
    //[HasPermission("delivery:update")]
    public async Task<ActionResult<UpdatedResponse>> PutDeliveryCarrier(Guid carrierId, DeliveryCarrierUpdateRequest request)
        => HandleResult(await _carrierService.UpdateDeliveryCarrier(carrierId, request));

    [HttpDelete("{carrierId}")]
    //[HasPermission("delivery:delete")]
    public async Task<IActionResult> DeleteDeliveryCarrier(Guid carrierId)
        => HandleResult(await _carrierService.DeleteDeliveryCarrier(carrierId));

    [HttpGet("{carrierId}")]
    //[HasPermission("delivery:read")]
    public async Task<ActionResult<DeliveryCarrierResponse>> GetDeliveryCarrier(Guid carrierId, CancellationToken ct)
        => HandleResult(await _carrierService.GetDeliveryCarrier(carrierId, ct));

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersList(CancellationToken ct)
        => HandleResult(await _carrierService.GetDeliveryCarriersList(ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _carrierService.GetSelectList(ct));

}

