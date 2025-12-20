using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO;
using ByteBuy.Core.DTO.Delivery;
using ByteBuy.Core.DTO.DeliveryCarrier;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;


namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryCarriersController 
    : CrudControllerBase<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest ,DeliveryCarrierResponse>
{
    private readonly IDeliveryCarrierService _carrierService;
    public DeliveryCarriersController(IDeliveryCarrierService carrierService) : base(carrierService)
         => _carrierService = carrierService;

    [HttpPost]
    //[HasPermission("delivery:create")]
    public override Task<ActionResult<CreatedResponse>> PostAsync(DeliveryCarrierAddRequest request)
        => base.PostAsync(request);

    [HttpPut("{id}")]
    //[HasPermission("delivery:update")]
    public override Task<ActionResult<UpdatedResponse>> PutAsync(Guid id, DeliveryCarrierUpdateRequest request)
        => base.PutAsync(id, request);

    [HttpDelete("{id}")]
    //[HasPermission("delivery:delete")]
    public override Task<IActionResult> DeleteAsync(Guid id)
        => base.DeleteAsync(id);

    [HttpGet("{id}")]
    //[HasPermission("delivery:read")]
    public override Task<ActionResult<DeliveryCarrierResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => base.GetByIdAsync(id, cancellationToken);

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<IEnumerable<DeliveryCarrierResponse>>> GetDeliveryCarriersList(CancellationToken ct)
        => HandleResult(await _carrierService.GetDeliveryCarriersList(ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IEnumerable<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _carrierService.GetSelectListAsync(ct));

}

