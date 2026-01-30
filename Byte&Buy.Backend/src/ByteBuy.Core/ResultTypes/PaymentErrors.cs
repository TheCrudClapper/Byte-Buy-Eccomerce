namespace ByteBuy.Core.ResultTypes;

public static class PaymentErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Payment.NotFound", "Payment of given id is not found");

    public static readonly Error InvalidPhoneNumber = Error.Validation
        ("PaymentDetails.PhoneNumber", "Given phone number is too short or in invalid format.");

    public static readonly Error InvalidCardNumber = Error.Validation
       ("PaymentDetails.CardNumber", "Given card number is invalid.");

    public static readonly Error InvalidCartHolder = Error.Validation
      ("PaymentDetails.CardHolder", "Given card holder is in invalid format.");
}
