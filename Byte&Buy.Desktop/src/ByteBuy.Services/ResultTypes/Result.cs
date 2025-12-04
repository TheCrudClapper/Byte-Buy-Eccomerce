using System.Diagnostics.CodeAnalysis;

namespace ByteBuy.Services.ResultTypes;

public class Result
{
    public bool Success { get; private set; }
    public Error? Error { get; private set; }

    public static Result Ok() => new Result { Success = true };
    public static Result Fail(Error error) =>
        new Result { Success = false, Error = error };
}


public class Result<TValue>
{
    public bool Success { get; }
    public TValue? _value;
    public Error? Error { get; }

    private Result(TValue value)
    {
        Success = true;
        _value = value;
    }

    private Result(Error error)
    {
        Success = false;
        Error = error;
    }

    public static Result<TValue> Ok(TValue value) => new(value);
    public static Result<TValue> Fail(Error error) => new(error);


    [NotNull]
    public TValue Value => Success ? _value! : throw new InvalidOperationException("The value of a failure can't be accessed");
}