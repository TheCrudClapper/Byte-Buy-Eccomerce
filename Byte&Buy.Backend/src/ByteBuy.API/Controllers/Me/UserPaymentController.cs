using ByteBuy.API.Attributes;
using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ByteBuy.API.Controllers.Me;

[Resource("user-payments")]
[ApiController]
[Route("api/me/payments")]
public class UserPaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    public UserPaymentController(IPaymentService paymentService)
       => _paymentService = paymentService;
    

    [HttpGet("{paymentId:guid}")]
    [HasPermission("user-payments:read:one")]
    public async Task<ActionResult<PaymentResponse>> GetPayment(Guid paymentId, CancellationToken ct)
        => HandleResult(await _paymentService.GetUnpaidPayment(CurrentUserId, paymentId, ct));

    [HttpPut("{paymentId:guid}/blik")]
    [HasPermission("user-payments:update:blik")]
    public async Task<ActionResult> PayUsingBlik(Guid paymentId, BlikPaymentRequest request)
        => HandleResult(await _paymentService.PayViaBlik(CurrentUserId, paymentId, request));

    [HttpPut("{paymentId:guid}/card")]
    [HasPermission("user-payments:update:card")]
    public async Task<ActionResult> PayUsingCard(Guid paymentId, CardPaymentRequest request)
        => HandleResult(await _paymentService.PayViaCard(CurrentUserId, paymentId, request));
}
