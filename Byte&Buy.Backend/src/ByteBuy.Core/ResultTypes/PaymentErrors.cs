namespace ByteBuy.Core.ResultTypes;

public static class PaymentErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Payment.NotFound", "Payment of given id is not found");

    public static readonly Error AlreadyPaid = new(
        ErrorType.Conflict, "Payment.Status", "You can't pay again for already completed payment!");

    public static readonly Error PaymentMethodsNotMatch = new(
        ErrorType.Validation, "Payment.PaymnetMethod",
        "Used payment method doesn't match the one specified in payment");

    public static readonly Error InvalidPhoneNumber = Error.Validation
        ("PaymentDetails.PhoneNumber", "Given phone number is too short or in invalid format.");

    public static readonly Error InvalidCardNumber = Error.Validation
       ("PaymentDetails.CardNumber", "Given card number is invalid.");

    public static readonly Error InvalidCartHolder = Error.Validation
      ("PaymentDetails.CardHolder", "Given card holder is in invalid format.");
}
