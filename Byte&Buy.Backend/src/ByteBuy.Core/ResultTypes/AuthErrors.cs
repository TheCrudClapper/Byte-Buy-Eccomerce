namespace ByteBuy.Core.ResultTypes;

public class AuthErrors
{
    public static readonly Error LoginFailed = new Error(
        400, "Wrong user credentials, try again");

    public static readonly Error AccountExists = new Error(
        400, "This Email is already taken");
}
