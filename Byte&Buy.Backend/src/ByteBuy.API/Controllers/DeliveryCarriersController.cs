using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Resource("deliverycarriers")]
[Route("api/[controller]")]
[ApiController]
public class DeliveryCarriersController
    : CrudControllerBase<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest, DeliveryCarrierResponse>
{
    private readonly IDeliveryCarrierService _carrierService;
    public DeliveryCarriersController(IDeliveryCarrierService carrierService) : base(carrierService)
         => _carrierService = carrierService;

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<IReadOnlyCollection<DeliveryCarrierResponse>>> GetDeliveryCarriersList(CancellationToken ct)
        => HandleResult(await _carrierService.GetDeliveryCarriersList(ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _carrierService.GetSelectListAsync(ct));

}

