using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Public.Payment;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IPaymentRepository paymentRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaymentResponse>> GetUnpaidPayment(Guid userId, Guid paymentId, CancellationToken ct)
    {
        var dto = await _paymentRepository.GetUnpaidUserPayment(userId, paymentId, ct);
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

        if (blikResult.IsFailure)
            return Result.Failure<Result>(blikResult.Error);

        var finalizeResult = await FinalizeAndPay(userId, payment, blikResult.Value);
        if (finalizeResult.IsFailure)
            return finalizeResult;

        await _paymentRepository.UpdateAsync(payment);
        await _unitOfWork.SaveChangesAsync();
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
            return Result.Failure<Result>(cardResult.Error);

        var finalizeResult = await FinalizeAndPay(userId, payment, cardResult.Value);
        if (finalizeResult.IsFailure)
            return finalizeResult;

        await _paymentRepository.UpdateAsync(payment);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<Result> FinalizeAndPay(Guid userId, Payment payment, PaymentDetails details)
    {
        var paymentResult = payment.FinalizePayment(details);
        if (paymentResult.IsFailure)
            return Result.Failure(paymentResult.Error);

        var orders = await _orderRepository.GetOrdersByPaymentIdAsync(userId, payment.Id);

        foreach (var order in orders)
        {
            var statusResult = order.PayForOrder();
            if (statusResult.IsFailure)
                return statusResult;
        }

        return Result.Success();
    }

}
