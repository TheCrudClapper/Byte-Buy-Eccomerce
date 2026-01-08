namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Enum used to map domain errors to http status codes
/// </summary>
public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    Unexpected
}

/// <summary>
/// Record used to describe errors occuring in domain and use cases.
/// </summary>
/// <param name="Type"></param>
/// <param name="Code"></param>
/// <param name="Description"></param>
public sealed record Error(ErrorType Type, string Code, string Description)
{
    public static readonly Error None 
        = new(ErrorType.Unexpected, "None", string.Empty);

    public static readonly Error NullValue 
        = new(ErrorType.Validation, "Common.NullValue", "NullValue");

    public static readonly Error NotFound 
        = new(ErrorType.NotFound, "Common.NotFound" ,"Resource of given Id doesn't exist");

    public static Error Validation(string code, string message)
        => new(ErrorType.Validation, code, message);
    public static Error Conflict(string code, string message) =>
       new(ErrorType.Conflict, code, message);
}

