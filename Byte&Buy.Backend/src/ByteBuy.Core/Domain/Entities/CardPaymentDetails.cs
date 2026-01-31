using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class CardPaymentDetails : PaymentDetails
{
    public string MaskedCardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;

    private CardPaymentDetails() { }

    private CardPaymentDetails(PaymentMethod method, string maskedCardNumber, string cardHolderName)
    {
        Method = method;
        MaskedCardNumber = maskedCardNumber;
        CardHolderName = cardHolderName;
    }

    public static Result<CardPaymentDetails> Create(PaymentMethod method, string cardNumber, string cardHolderName)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 16 || cardNumber.Length > 19)
            return Result.Failure<CardPaymentDetails>(PaymentErrors.InvalidCardNumber);

        if (string.IsNullOrWhiteSpace(cardHolderName))
            return Result.Failure<CardPaymentDetails>(PaymentErrors.InvalidCartHolder);

        return new CardPaymentDetails(method, cardNumber, cardHolderName);
    }
}
