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
}

