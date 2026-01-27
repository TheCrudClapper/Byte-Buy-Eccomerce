using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CheckoutController : BaseApiController
{
    private readonly ICheckoutService _checkoutService;
    public CheckoutController(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpGet]
    public async Task<ActionResult<CheckoutResponse>> GetCheckout(CancellationToken ct)
        => HandleResult(await _checkoutService.GetCheckout(CurrentUserId));
}
