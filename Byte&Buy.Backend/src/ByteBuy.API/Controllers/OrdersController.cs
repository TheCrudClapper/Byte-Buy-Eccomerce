using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Order.Common;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IOrderReadService _orderReadService;
    public OrdersController(IOrderService orderService, IOrderReadService orderReadService)
    {
        _orderService = orderService;
        _orderReadService = orderReadService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderCreatedReponse>> PostOrder(OrderAddRequest request)
        => HandleResult(await _orderService.AddAsync(CurrentUserId, request));

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> ReturnOrder(Guid orderId)
        => HandleResult(await _orderService.ReturnOrder(CurrentUserId, orderId));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserOrderListResponse>>> GetUserOrders(CancellationToken ct)
        => HandleResult(await _orderReadService.GetAllUserOrders(CurrentUserId, ct));
}
