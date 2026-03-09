using ByteBuy.Core.Domain.Payments;
using ByteBuy.Core.Domain.RepositoryContracts.Base;
using ByteBuy.Core.DTO.Public.Payment;

namespace ByteBuy.Core.Domain.RepositoryContracts;

public interface IPaymentRepository : IRepositoryBase<Payment>
{
    Task<Payment?> GetPaymentByUserId(Guid userId, Guid paymentId, CancellationToken ct = default);
    Task<PaymentResponse?> GetUnpaidUserPayment(Guid userId, Guid paymentId, CancellationToken ct = default);
}
