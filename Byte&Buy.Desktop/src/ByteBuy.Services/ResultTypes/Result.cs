using ByteBuy.Services.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Services.ResultTypes;

public class Result
{
    public bool Success { get; private set; }
    public Error? Error { get; private set; }

    public static Result Ok() => new Result { Success = true };
    public static Result Fail(Error error) =>
        new Result { Success = false, Error = error };
}


public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public Error? Error { get; }

    private Result(T value)
    {
        Success = true;
        Value = value;
    }

    private Result(Error error)
    {
        Success = false;
        Error = error;
    }

    public static Result<T> Ok(T value) => new(value);
    public static Result<T> Fail(Error error) => new(error);
}

public static class ResultExtensions
{
    public static Result<T> Map<T>(this Result<T> result)
        => result.Success ? Result<T>.Ok(result.Value!) : Result<T>.Fail(result.Error!);
    
    public static Result Map(this Result result)
        => result.Success ? Result.Ok() : Result.Fail(result.Error!);
}