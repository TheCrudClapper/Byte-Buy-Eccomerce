using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace ByteBuy.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : BaseApiController
{
    private readonly IOrderCreateService _orderCreationService;
    private readonly IOrderService _orderService;
    public OrdersController(IOrderCreateService orderService, IOrderService orderReadService)
    {
        _orderCreationService = orderService;
        _orderService = orderReadService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderCreatedReponse>> PostOrder(OrderAddRequest request)
        => HandleResult(await _orderCreationService.AddAsync(CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserOrderListResponse>>> GetUserOrders(CancellationToken ct)
        => HandleResult(await _orderService.GetUserOrdersAsync(CurrentUserId, ct));

    [HttpGet("details/{orderId:guid}")]
    public async Task<ActionResult<OrderDetailsResponse>> GetOrderDetails(Guid orderId, CancellationToken ct)
        => HandleResult(await _orderService.GetOrderDetailsAsync(CurrentUserId, orderId, ct));

    [HttpPut("{orderId:guid}/cancel")]
    public async Task<ActionResult<UpdatedResponse>> CancelOrder(Guid orderId)
        => HandleResult(await _orderService.CancelOrder(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/return")]
    public async Task<ActionResult<UpdatedResponse>> ReturnOrder(Guid orderId)
        => HandleResult(await _orderService.ReturnOrder(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/ship")]
    public async Task<ActionResult<UpdatedResponse>> ShipOrder(Guid orderId)
        => HandleResult(await _orderService.ShipOrderAsPrivateSeller(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/deliver")]
    public async Task<ActionResult<UpdatedResponse>> DeliverOrder(Guid orderId)
       => HandleResult(await _orderService.DeliverOrderAsPrivateSeller(CurrentUserId, orderId));

    [HttpGet("dashboard")]
    public async Task<ActionResult<UpdatedResponse>> GetDashboardOrders(CancellationToken ct)
        => HandleResult(await _orderService.GetDashboardOrdersAsync(ct));

    [HttpGet("seller")]
    public async Task<ActionResult<IReadOnlyCollection<UserOrderListResponse>>> GetSellerOrders(CancellationToken ct)
       => HandleResult(await _orderService.GetSellerOrdersAsync(CurrentUserId, ct));
}
