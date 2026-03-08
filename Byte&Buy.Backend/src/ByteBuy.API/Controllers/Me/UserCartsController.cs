using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Cart;
using ByteBuy.Core.DTO.Public.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-carts")]
[Route("api/me/carts")]
[ApiController]
public class UserCartsController : BaseApiController
{
    private readonly ICartService _cartService;
    public UserCartsController(ICartService cartService)
        => _cartService = cartService;


    [HttpDelete("{cartOfferId:guid}")]
    [HasPermission("user-carts:delete:one")]
    public async Task<IActionResult> DeleteCartOfferAsync(Guid cartOfferId)
        => HandleResult(await _cartService.DeleteCartOfferAsync(CurrentUserId, cartOfferId));

    [HttpPost("rent-offer")]
    [HasPermission("user-carts:create:rent")]
    public async Task<ActionResult<CreatedResponse>> PostRentCartOfferAsync(RentCartOfferAddRequest request)
        => HandleResult(await _cartService.AddRentCartOfferAsync(CurrentUserId, request));

    [HttpPost("sale-offer")]
    [HasPermission("user-carts:create:sale")]
    public async Task<ActionResult<CreatedResponse>> PostSaleCartOfferAsync(SaleCartOfferAddRequest request)
        => HandleResult(await _cartService.AddSaleCartOfferAsync(CurrentUserId, request));

    [HttpPut("sale-offer/{cartOfferId:guid}")]
    [HasPermission("user-carts:update:sale")]
    public async Task<ActionResult<CreatedResponse>> PutSaleCartOfferAsync(Guid cartOfferId, SaleCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateSaleCartOfferAsync(CurrentUserId, cartOfferId, request));

    [HttpPut("rent-offer/{cartOfferId:guid}")]
    [HasPermission("user-carts:update:rent")]
    public async Task<ActionResult<CreatedResponse>> PutRentCartOfferAsync(Guid cartOfferId, RentCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateRentCartOfferAsync(CurrentUserId, cartOfferId, request));

    [HttpGet]
    [HasPermission("user-carts:read:one")]
    public async Task<ActionResult<CartResponse>> GetCartAsync(CancellationToken ct = default)
        => HandleResult(await _cartService.GetCartAsync(CurrentUserId, ct));

    [HttpDelete("clear")]
    [HasPermission("user-carts:delete:all")]
    public async Task<ActionResult> ClearCartAsync()
        => HandleResult(await _cartService.ClearCartAsync(CurrentUserId));
}
