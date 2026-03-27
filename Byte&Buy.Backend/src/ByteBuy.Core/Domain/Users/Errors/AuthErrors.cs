using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Users.Errors;

/// <summary>
/// Defines errors that occur while working with accounts, authentication.
/// </summary>
public static class AuthErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Auth.NotFound", "User of given credentials is not found");

    public static readonly Error LoginFailed = new(
        ErrorType.Unauthorized, "Auth.LoginFailed", "Wrong user credentials, try again");

    public static readonly Error EmailAlreadyTaken = new(
       ErrorType.Conflict, "Auth.EmailAlreadyTaken", "This Email is already taken");

    public static readonly Error AccessDenied = new(
        ErrorType.Unauthorized, "Auth.AccessDenied", "User of given credentials can't access this resource");

    public static readonly Error PasswordsDontMatch = new(
        ErrorType.Conflict, "Auth.PasswordsDontMatch", "Confirmation and new passwords doesn't match");

    public static readonly Error FailedToRegisterUser = new(
        ErrorType.Unexpected, "Auth.Register", "Failed to register user, please try again.");
}
