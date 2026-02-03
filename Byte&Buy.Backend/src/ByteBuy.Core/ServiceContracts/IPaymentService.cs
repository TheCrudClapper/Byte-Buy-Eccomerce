using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.ServiceContracts;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> GetUnpaidPayment(Guid userId, Guid paymentId, CancellationToken ct = default);
    Task<Result> PayViaBlik(Guid userId, Guid paymentId, BlikPaymentRequest request);
    Task<Result> PayViaCard(Guid userId, Guid paymentId, CardPaymentRequest request);
}
