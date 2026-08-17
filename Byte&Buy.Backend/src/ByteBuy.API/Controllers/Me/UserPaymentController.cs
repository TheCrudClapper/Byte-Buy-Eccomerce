using ByteBuy.Core.DTO.Public.Payment;
namespace ByteBuy.API.Controllers.Me;

[Resource("user-payments")]
[Route("api/me/payments")]
[ApiController]
public class UserPaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    public UserPaymentController(IPaymentService paymentService)
       => _paymentService = paymentService;


    [HttpGet("{paymentId:guid}")]
    [HasPermission("{resource}:read:one")]
    public async Task<ActionResult<PaymentResponse>> GetPaymentAsync(Guid paymentId, CancellationToken ct)
        => HandleResult(await _paymentService.GetUnpaidPaymentAsync(CurrentUserId, paymentId, ct));

    [HttpPut("{paymentId:guid}/blik")]
    [HasPermission("{resource}:update:blik")]
    public async Task<ActionResult> PayUsingBlikAsync(Guid paymentId, BlikPaymentRequest request)
        => HandleResult(await _paymentService.PayViaBlikAsync(CurrentUserId, paymentId, request));

    [HttpPut("{paymentId:guid}/card")]
    [HasPermission("{resource}:update:card")]
    public async Task<ActionResult> PayUsingCardAsync(Guid paymentId, CardPaymentRequest request)
        => HandleResult(await _paymentService.PayViaCardAsync(CurrentUserId, paymentId, request));
}
