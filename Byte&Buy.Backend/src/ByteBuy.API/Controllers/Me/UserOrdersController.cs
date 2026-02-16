using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Order;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.Filtration.Order;
using ByteBuy.Core.Pagination;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Route("api/me/orders")]
[ApiController]
public class UserOrdersController : BaseApiController
{
    private readonly IOrderCreateService _orderCreationService;
    private readonly IOrderService _orderService;
    public UserOrdersController(IOrderCreateService orderService, IOrderService orderReadService)
    {
        _orderCreationService = orderService;
        _orderService = orderReadService;
    }

    [HttpPost]
    public async Task<ActionResult<OrderCreatedReponse>> PostOrderAsync(OrderAddRequest request)
        => HandleResult(await _orderCreationService.AddAsync(CurrentUserId, request));

    [HttpGet]
    public async Task<ActionResult<PagedList<UserOrderListResponse>>> GetOrdersAsync([FromQuery] UserOrderListQuery queryParams, CancellationToken ct)
        => HandleResult(await _orderService.GetUserOrdersAsync(queryParams, CurrentUserId, ct));

    [HttpGet("seller")]
    public async Task<ActionResult<PagedList<UserOrderListResponse>>> GetSellerOrdersAsync([FromQuery] UserOrderSellerListQuery queryParams, CancellationToken ct)
        => HandleResult(await _orderService.GetSellerOrdersAsync(queryParams, CurrentUserId, ct));

    [HttpGet("details/{orderId:guid}")]
    public async Task<ActionResult<OrderDetailsResponse>> GetOrderDetailsAsync(Guid orderId, CancellationToken ct)
        => HandleResult(await _orderService.GetOrderDetailsAsync(CurrentUserId, orderId, ct));

    [HttpPut("{orderId:guid}/cancel")]
    public async Task<ActionResult<UpdatedResponse>> CancelOrderAsync(Guid orderId)
        => HandleResult(await _orderService.CancelOrder(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/return")]
    public async Task<ActionResult<UpdatedResponse>> ReturnOrderAsync(Guid orderId)
        => HandleResult(await _orderService.ReturnOrder(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/ship")]
    public async Task<ActionResult<UpdatedResponse>> ShipOrderAsync(Guid orderId)
        => HandleResult(await _orderService.ShipOrderAsPrivateSeller(CurrentUserId, orderId));

    [HttpPut("{orderId:guid}/deliver")]
    public async Task<ActionResult<UpdatedResponse>> DeliverOrderAsync(Guid orderId)
       => HandleResult(await _orderService.DeliverOrderAsPrivateSeller(CurrentUserId, orderId));
}
