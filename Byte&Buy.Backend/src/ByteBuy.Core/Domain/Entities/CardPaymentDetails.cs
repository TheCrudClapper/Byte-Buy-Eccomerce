using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class CardPaymentDetails : PaymentDetails
{
    public string MaskedCardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;

    private CardPaymentDetails() { }

    private CardPaymentDetails(Guid paymentId, PaymentMethod method, string maskedCardNumber, string cardHolderName) : base(method, paymentId)
    {
        var num = maskedCardNumber.Remove(0, maskedCardNumber.Length - 4);
        MaskedCardNumber = num.PadLeft(maskedCardNumber.Length, '*');
        CardHolderName = cardHolderName;
    }

    public static Result<CardPaymentDetails> Create(Guid paymentId, PaymentMethod method, string cardNumber, string cardHolderName)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 16 || cardNumber.Length > 19)
            return Result.Failure<CardPaymentDetails>(PaymentErrors.InvalidCardNumber);

        if (string.IsNullOrWhiteSpace(cardHolderName) || cardHolderName.Length > 30)
            return Result.Failure<CardPaymentDetails>(PaymentErrors.InvalidCartHolder);

        return new CardPaymentDetails(paymentId, method, cardNumber, cardHolderName);
    }
}
