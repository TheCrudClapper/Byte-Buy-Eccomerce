using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

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
        => HandleResult(await _orderService.GetAllUserOrders(CurrentUserId, ct));

    [HttpGet("details/{orderId:guid}")]
    public async Task<ActionResult<OrderDetailsResponse>> GetOrderDetails(Guid orderId, CancellationToken ct)
        => HandleResult(await _orderService.GetOrderDetails(orderId, ct));


}
