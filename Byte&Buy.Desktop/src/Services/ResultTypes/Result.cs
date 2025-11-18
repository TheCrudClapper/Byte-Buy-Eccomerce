using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.Services.ResultTypes;

public class Result
{
    public bool Success { get; private set; }
    public ProblemDetails? Error { get; private set; }

    public static Result Ok() => new Result { Success = true };
    public static Result Fail(ProblemDetails error) =>
        new Result { Success = false, Error = error };
}


public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public ProblemDetails? Error { get; }

    private Result(T value)
    {
        Success = true;
        Value = value;
    }

    private Result(ProblemDetails? error)
    {
        Success = false;
        Error = error;
    }

    public static Result<T> Ok(T value) => new(value);
    public static Result<T> Fail(ProblemDetails? error) => new(error);
}