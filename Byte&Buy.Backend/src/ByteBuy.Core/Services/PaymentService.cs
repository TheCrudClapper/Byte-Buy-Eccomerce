using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
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

    public async Task<Result> PayViaBlik(Guid userId, Guid paymentId, BlikPaymentRequest request)
    {
        var payment = await _paymentRepository.GetPaymentByUserId(userId, paymentId);
        if (payment is null)
            return Result.Failure(PaymentErrors.NotFound);

        if (payment.Method != PaymentMethod.Blik)
            return Result.Failure(PaymentErrors.PaymentMethodsNotMatch);

        var blikResult = BlikPaymentDetails.Create(
            payment.Id,
            payment.Method,
            request.PhoneNumber);

        if(blikResult.IsFailure)
            return Result.Failure<Result>(PaymentErrors.NotFound);

        var paymentResult = payment.FinalizePayment(blikResult.Value);
        if (paymentResult.IsFailure)
            return Result.Failure(paymentResult.Error);

        var orders = await _orderRepository.GetOrdersByPaymentId(userId, paymentId);

        foreach(var order in orders)
            order.PayForOrder();

        await _paymentRepository.UpdateAsync(payment);
        await _paymentRepository.CommitAsync();
        return Result.Success();
    }

    public async Task<Result> PayViaCard(Guid userId, Guid paymentId, CardPaymentRequest request)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment is null)
            return Result.Failure<Result>(PaymentErrors.NotFound);

        if (payment.Method != PaymentMethod.Card)
            return Result.Failure(PaymentErrors.PaymentMethodsNotMatch);

        var cardResult = CardPaymentDetails.Create(
            payment.Id,
            payment.Method,
            request.CardNumber,
            request.CardHolderName);

        if (cardResult.IsFailure)
            return Result.Failure<Result>(PaymentErrors.NotFound);

        var paymentResult = payment.FinalizePayment(cardResult.Value);
        if (paymentResult.IsFailure)
            return Result.Failure(paymentResult.Error);

        var orders = await _orderRepository.GetOrdersByPaymentId(userId, paymentId);

        foreach (var order in orders)
            order.PayForOrder();

        await _paymentRepository.UpdateAsync(payment);
        await _paymentRepository.CommitAsync();
        return Result.Success();
    }
}
