
using ByteBuy.Core.DTO.Public.Checkout;

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
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<CheckoutResponse>> GetCheckoutAsync(CancellationToken ct)
        => HandleResult(await _checkoutService.GetCheckoutAsync(CurrentUserId, ct));
}
