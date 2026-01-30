using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderCreatedReponse>> PostOrder(OrderAddRequest request)
        => HandleResult(await _orderService.AddAsync(CurrentUserId, request));

    [HttpPut]
    public async Task<ActionResult<UpdatedResponse>> ReturnOrder(Guid orderId)
        => HandleResult(await _orderService.ReturnOrder(CurrentUserId, orderId));
}
