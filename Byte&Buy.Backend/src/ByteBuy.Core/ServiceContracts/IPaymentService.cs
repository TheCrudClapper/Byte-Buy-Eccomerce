using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> GetPayment(Guid paymentId);
    Task<Result> PayViaBlik(Guid paymentId, BlikPaymentRequest request);
    Task<Result> PayViaCard(Guid paymentId, CardPaymentRequest request);
}
