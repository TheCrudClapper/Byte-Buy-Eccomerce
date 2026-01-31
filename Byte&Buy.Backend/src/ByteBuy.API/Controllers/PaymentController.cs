using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController : BaseApiController
{
    private readonly IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{paymentId:guid}")]
    public async Task<ActionResult<PaymentResponse>> GetPayment(Guid paymentId)
        => HandleResult(await _paymentService.GetPayment(paymentId));

    [HttpPut("{paymentId:guid}/blik")]
    public async Task<ActionResult> PayUsingBlik(Guid paymentId, BlikPaymentRequest request)
        => HandleResult(await _paymentService.PayViaBlik(CurrentUserId, paymentId, request));

    [HttpPut("{paymentId:guid}/card")]
    public async Task<ActionResult> PayUsingCard(Guid paymentId, CardPaymentRequest request)
        => HandleResult(await _paymentService.PayViaCard(CurrentUserId, paymentId, request));
}
