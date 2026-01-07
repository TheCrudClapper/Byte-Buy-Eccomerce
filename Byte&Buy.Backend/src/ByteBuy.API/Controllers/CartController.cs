using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Cart;
using ByteBuy.Core.DTO.SaleOffer;
using ByteBuy.Core.DTO.Shared;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : BaseApiController
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService = cartService;    
    }

    [HttpDelete("{cartOfferId:guid}")]
    public async Task<IActionResult> DeleteCartOffer(Guid cartOfferId)
        => HandleResult(await _cartService.DeleteCartOffer(CurrentUserId, cartOfferId));

    [HttpPost("rent-offer")]
    public async Task<ActionResult<CreatedResponse>> PostRentCartOffer(RentCartOfferAddRequest request)
        => HandleResult(await _cartService.AddRentCartOffer(CurrentUserId, request));

    [HttpPost("sale-offer")]
    public async Task<ActionResult<CreatedResponse>> PostSaleCartOffer(SaleCartOfferAddRequest request)
        => HandleResult(await _cartService.AddSaleCartOffer(CurrentUserId, request));

    [HttpPut("sale-offer/{cartOfferId:guid}")]
    public async Task<ActionResult<CreatedResponse>> PutSaleCartOffer(Guid cartOfferId, SaleCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateSaleCartOffer(CurrentUserId, cartOfferId, request));

    [HttpPut("rent-offer/{cartOfferId:guid}")]
    public async Task<ActionResult<CreatedResponse>> PutRentCartOffer(Guid cartOfferId, RentCartOfferUpdateRequest request)
        => HandleResult(await _cartService.UpdateRentCartOffer(CurrentUserId, cartOfferId, request));
}
