namespace ByteBuy.Core.ResultTypes;

public static class CommonUserErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "User.NotFound", "User is not found");

    public static readonly Error FirstNameInvalid = Error.Validation(
        "User.CustomersFullName", "First name is required.");

    public static readonly Error LastNameInvalid = Error.Validation(
        "User.LastName", "Last name is required.");

    public static readonly Error EmailInvalid = Error.Validation(
        "User.Email", "Given email is not a valid email address");

    public static readonly Error PhoneNumberInvalid = Error.Validation(
        "User.PhoneNumber", "Phone number can't be a whitespace and not longer that 15 characters");
}
