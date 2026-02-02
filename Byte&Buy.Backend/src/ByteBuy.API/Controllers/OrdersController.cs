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
    private readonly IOrderCreateService _orderService;
    private readonly IOrderService _orderReadService;
    public OrdersController(IOrderCreateService orderService, IOrderService orderReadService)
    {
        _orderService = orderService;
        _orderReadService = orderReadService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderCreatedReponse>> PostOrder(OrderAddRequest request)
        => HandleResult(await _orderService.AddAsync(CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserOrderListResponse>>> GetUserOrders(CancellationToken ct)
        => HandleResult(await _orderReadService.GetAllUserOrders(CurrentUserId, ct));

}
