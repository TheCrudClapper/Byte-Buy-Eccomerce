using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/orders")]
[ApiController]
public class CompanyOrdersController : BaseApiController
{
    private IOrderService _orderService;
    public CompanyOrdersController(IOrderService orderService)
        => _orderService = orderService;

    [HttpGet("company")]
    public async Task<ActionResult<PagedList<CompanyOrderListResponse>>> GetCompanyOrders([FromQuery] OrderCompanyListQuery queryParams, CancellationToken ct)
       => HandleResult(await _orderService.GetCompanyOrdersListAsync(queryParams, ct));

    [HttpGet("details/{orderId:guid}/company")]
    public async Task<ActionResult<OrderDetailsResponse>> GetCompanyOrderDetails(Guid orderId, CancellationToken ct)
    => HandleResult(await _orderService.GetCompanyOrderDetailsAsync(orderId, ct));

    [HttpPut("{orderId:guid}/ship/company")]
    public async Task<ActionResult<UpdatedResponse>> ShipCompanyOrder(Guid orderId)
        => HandleResult(await _orderService.ShipOrderAsCompany(orderId));

    [HttpPut("{orderId:guid}/deliver/company")]
    public async Task<ActionResult<UpdatedResponse>> DeliverOrder(Guid orderId)
       => HandleResult(await _orderService.DeliverOrderAsCompany(orderId));

}
