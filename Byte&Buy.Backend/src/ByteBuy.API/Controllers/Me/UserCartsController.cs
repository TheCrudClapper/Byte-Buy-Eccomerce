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
    public async Task<IActionResult> DeleteCartOffer(Guid cartOfferId)
        => HandleResult(await _cartService.DeleteCartOffer(CurrentUserId, cartOfferId));

    [HttpPost("rent-offer")]
    [HasPermission("user-carts:create:rent")]
    public async Task<ActionResult<CreatedResponse>> PostRentCartOffer(RentCartOfferAddRequest request)
        => HandleResult(await _cartService.AddRentCartOffer(CurrentUserId, request));

    [HttpPost("sale-offer")]
    [HasPermission("user-carts:create:sale")]
    public async Task<ActionResult<CreatedResponse>> PostSaleCartOffer(SaleCartOfferAddRequest request)
        => HandleResult(await _cartService.AddSaleCartOffer(CurrentUserId, request));

    [HttpPut("sale-offer/{cartOfferId:guid}")]
    [HasPermission("user-carts:update:sale")]
    public async Task<ActionResult<CreatedResponse>> PutSaleCartOffer(Guid cartOfferId, SaleCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateSaleCartOffer(CurrentUserId, cartOfferId, request));

    [HttpPut("rent-offer/{cartOfferId:guid}")]
    [HasPermission("user-carts:update:rent")]
    public async Task<ActionResult<CreatedResponse>> PutRentCartOffer(Guid cartOfferId, RentCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateRentCartOffer(CurrentUserId, cartOfferId, request));

    [HttpGet]
    [HasPermission("user-carts:read:one")]
    public async Task<ActionResult<CartResponse>> GetCart(CancellationToken ct = default)
        => HandleResult(await _cartService.GetCart(CurrentUserId, ct));

    [HttpDelete("clear")]
    [HasPermission("user-carts:delete:all")]
    public async Task<ActionResult> ClearCart()
        => HandleResult(await _cartService.ClearCart(CurrentUserId));
}
