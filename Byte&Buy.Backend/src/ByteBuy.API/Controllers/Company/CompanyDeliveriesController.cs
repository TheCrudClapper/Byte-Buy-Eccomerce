using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("deliveries")]
[Route("api/company/deliveries")]
[ApiController]
public class CompanyDeliveriesController
    : CrudControllerBase<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>
{
    private readonly IDeliveryService _deliveryService;
    public CompanyDeliveriesController(IDeliveryService deliveryService) : base(deliveryService)
        => _deliveryService = deliveryService;


    [HttpGet("list")]
    //[HasPermission("delivery:read:list")]
    public async Task<ActionResult<PagedList<DeliveryListResponse>>> GetDeliveriesList([FromQuery] DeliveryListQuery query, CancellationToken ct)
        => HandleResult(await _deliveryService.GetDeliveriesListAsync(query, ct));

    [HttpGet("sizes/list")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectList()
       => HandleResult(_deliveryService.GetParcelLockerSizes());

    [HttpGet("channels/list")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectList()
        => HandleResult(_deliveryService.GetDeliveryChannels());
}
