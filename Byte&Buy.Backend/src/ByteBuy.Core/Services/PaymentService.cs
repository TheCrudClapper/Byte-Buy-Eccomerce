using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using static ByteBuy.Core.Specification.PaymentSpecification;

namespace ByteBuy.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    public PaymentService(IPaymentRepository paymentRepository,
        IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<PaymentResponse>> GetPayment(Guid paymentId)
    {
        var spec = new PaymentResponseSpec(paymentId);
        var dto = await _paymentRepository.GetBySpecAsync(spec);

        return dto is null
            ? Result.Failure<PaymentResponse>(PaymentErrors.NotFound)
            : dto;
    }

    public Task<Result> PayViaBlik(Guid paymentId, BlikPaymentRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> PayViaCard(Guid paymentId, CardPaymentRequest request)
    {
        throw new NotImplementedException();
    }
}
