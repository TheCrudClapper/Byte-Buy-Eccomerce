using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.DeliveryCarrier;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.DeliveryCarrier;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("delivery-carriers")]
[Route("api/company/delivery-carriers")]
[ApiController]
public class CompanyDeliveryCarriersController
    : CrudControllerBase<Guid, DeliveryCarrierAddRequest, DeliveryCarrierUpdateRequest, DeliveryCarrierResponse>
{
    private readonly IDeliveryCarrierService _carrierService;
    public CompanyDeliveryCarriersController(IDeliveryCarrierService carrierService) : base(carrierService)
         => _carrierService = carrierService;

    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<PagedList<DeliveryCarrierResponse>>> GetDeliveryCarriersList([FromQuery] DeliveryCarriersListQuery queryParams, CancellationToken ct)
        => HandleResult(await _carrierService.GetDeliveryCarriersList(queryParams, ct));

    [HttpGet("options")]
    //[HasPermission("delivery:read:options")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<Guid>>>> GetSelectList(CancellationToken ct)
        => HandleResult(await _carrierService.GetSelectListAsync(ct));

}

