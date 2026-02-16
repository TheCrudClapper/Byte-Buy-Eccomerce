using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Checkout;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Me;

[Resource("user-checkout")]
[Route("api/me/checkout")]
[ApiController]
public class UserCheckoutController : BaseApiController
{
    private readonly ICheckoutService _checkoutService;
    public UserCheckoutController(ICheckoutService checkoutService)
      => _checkoutService = checkoutService;

    [HttpGet]
    [HasPermission("user-checkout:read:one")]
    public async Task<ActionResult<CheckoutResponse>> GetCheckout(CancellationToken ct)
        => HandleResult(await _checkoutService.GetCheckout(CurrentUserId, ct));
}
