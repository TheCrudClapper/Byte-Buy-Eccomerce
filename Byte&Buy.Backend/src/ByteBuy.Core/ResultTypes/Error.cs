namespace ByteBuy.Core.ResultTypes;

public sealed record Error(int ErrorCode, string Description)
{
    public static readonly Error None = new(500, string.Empty);
    public static readonly Error NullValue = new(404, "Null value");

    public static readonly Error NotFound = new(404, "Resource of given Id doesn't exist");
    public static Error Validation(string message) => new(400, message);
}

