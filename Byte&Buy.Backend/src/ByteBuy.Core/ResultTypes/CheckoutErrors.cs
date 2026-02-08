namespace ByteBuy.Core.ResultTypes;

public static class CheckoutErrors
{
    public static readonly Error UserDataNotFound = Error.Validation(
        "Checkout.UserData", "User data such: phone, first, last name email not found, add them to proceed");
}
