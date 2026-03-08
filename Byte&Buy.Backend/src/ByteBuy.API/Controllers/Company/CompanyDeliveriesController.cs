using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Delivery;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Delivery;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Resource("company-deliveries")]
[Route("api/company/deliveries")]
[ApiController]
public class CompanyDeliveriesController
    : CrudControllerBase<Guid, DeliveryAddRequest, DeliveryUpdateRequest, DeliveryResponse>
{
    private readonly IDeliveryService _deliveryService;
    public CompanyDeliveriesController(IDeliveryService deliveryService) : base(deliveryService)
        => _deliveryService = deliveryService;


    [HttpGet("list")]
    [HasPermission("company-deliveries:read:many")]
    public async Task<ActionResult<PagedList<DeliveryListResponse>>> GetDeliveriesListAsync([FromQuery] DeliveryListQuery query, CancellationToken ct)
        => HandleResult(await _deliveryService.GetDeliveriesListAsync(query, ct));

    [HttpGet("sizes/list")]
    [HasPermission("company-deliveries:read:sizes")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetParcelLockerSizesSelectListAsync()
       => HandleResult(_deliveryService.GetParcelLockerSizes());

    [HttpGet("channels/list")]
    [HasPermission("company-deliveries:read:channels")]
    public async Task<ActionResult<IReadOnlyCollection<SelectListItemResponse<int>>>> GetDeliveryChannelsSelectListAsync()
        => HandleResult(_deliveryService.GetDeliveryChannels());
}
