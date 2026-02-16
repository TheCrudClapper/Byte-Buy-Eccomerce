using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Route("api/company/orders")]
[ApiController]
public class CompanyOrdersController : BaseApiController
{
    private IOrderService _orderService;
    public CompanyOrdersController(IOrderService orderService)
        => _orderService = orderService;

    [HttpGet]
    public async Task<ActionResult<PagedList<CompanyOrderListResponse>>> GetCompanyOrdersListAsync([FromQuery] OrderCompanyListQuery queryParams, CancellationToken ct)
       => HandleResult(await _orderService.GetCompanyOrdersListAsync(queryParams, ct));

    [HttpGet("details/{orderId:guid}")]
    public async Task<ActionResult<OrderDetailsResponse>> GetCompanyOrderDetailsAsync(Guid orderId, CancellationToken ct)
    => HandleResult(await _orderService.GetCompanyOrderDetailsAsync(orderId, ct));

    [HttpPut("{orderId:guid}/ship")]
    public async Task<ActionResult<UpdatedResponse>> ShipCompanyOrderAsync(Guid orderId)
        => HandleResult(await _orderService.ShipOrderAsCompany(orderId));

    [HttpPut("{orderId:guid}/deliver")]
    public async Task<ActionResult<UpdatedResponse>> DeliverOrderAsync(Guid orderId)
       => HandleResult(await _orderService.DeliverOrderAsCompany(orderId));

    [HttpGet("dashboard")]
    public async Task<ActionResult<UpdatedResponse>> GetDashboardOrdersAsync(CancellationToken ct)
        => HandleResult(await _orderService.GetDashboardOrdersAsync(ct));
}
