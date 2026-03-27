using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Extensions;

public static class ResultExtension
{
    // Extension that allows to return parent's result 
    public static Result<TBase> Upcast<TBase, TDerived>(
        this Result<TDerived> result)
        where TDerived : TBase
    {
        if (result.IsFailure)
            return Result.Failure<TBase>(result.Error);

        return Result.Success<TBase>(result.Value);
    }
}
