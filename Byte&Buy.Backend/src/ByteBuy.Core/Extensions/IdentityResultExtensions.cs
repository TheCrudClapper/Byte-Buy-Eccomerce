using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
            return Result.Success();

        var errorDescription = string.Join("; ", identityResult.Errors.Select(e => e.Description));
        return Result.Failure(Error.Validation(errorDescription));
    }
    public static Result<T> ToResult<T>(this IdentityResult identityResult, T value)
    {
        return identityResult.Succeeded
            ? Result.Success(value)
            : Result.Failure<T>(Error.Validation(string.Join("; ", identityResult.Errors.Select(e => e.Description))));
    }
    public static Result<T> ToResult<T>(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
            throw new InvalidOperationException("Cannot create Result<T> without a value on success.");

        var errorDescription = string.Join("; ", identityResult.Errors.Select(e => e.Description));
        return Result.Failure<T>(Error.Validation(errorDescription));
    }

}

