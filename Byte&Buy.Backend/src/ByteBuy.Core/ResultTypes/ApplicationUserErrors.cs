namespace ByteBuy.Core.ResultTypes;

public static class ApplicationUserErrors
{
    public static readonly Error InvalidPassword = new Error(
        400, "Invalid user password");

    public static readonly Error PasswordsDontMatch = new Error(
        400, "Confirmation and new passwords doesn't match");
}
