using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class BlikPaymentDetails : PaymentDetails
{
    public string PhoneNumber { get; private set; } = null!;

    private BlikPaymentDetails() { }

    private BlikPaymentDetails(Guid paymentId, PaymentMethod method, string phoneNumber) : base(method, paymentId)
    {
        PhoneNumber = phoneNumber;
    }

    public static Result<BlikPaymentDetails> Create(Guid paymentId, PaymentMethod method, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 9 || phoneNumber.Length > 15)
            return Result.Failure<BlikPaymentDetails>(PaymentErrors.InvalidPhoneNumber);

        return new BlikPaymentDetails(paymentId, method, phoneNumber);
    }

}
