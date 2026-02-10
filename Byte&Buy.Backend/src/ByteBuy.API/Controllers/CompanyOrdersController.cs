using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Authorize]
[Route("api/orders")]
[ApiController]
public class CompanyOrdersController : BaseApiController
{
    private IOrderService _orderService;
    public CompanyOrdersController(IOrderService orderService)
        => _orderService = orderService;

    [HttpGet("company")]
    public async Task<ActionResult<IReadOnlyCollection<CompanyOrderListResponse>>> GetCompanyOrders(CancellationToken ct)
       => HandleResult(await _orderService.GetCompanyOrdersListAsync(ct));

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
