using ByteBuy.Core.Domain.Shared.ResultTypes;
using ByteBuy.Core.DTO.Public.Payment;

namespace ByteBuy.Core.ServiceContracts;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> GetUnpaidPaymentAsync(Guid userId, Guid paymentId, CancellationToken ct = default);
    Task<Result> PayViaBlikAsync(Guid userId, Guid paymentId, BlikPaymentRequest request);
    Task<Result> PayViaCardAsync(Guid userId, Guid paymentId, CardPaymentRequest request);
}
