namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with condition aggregate
/// </summary>
public static class ConditionErrors
{
    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Condition.AlreadyExits", "Condition with data already exists");

    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Condition.NotFound", "Condition is not found");

    public static readonly Error HasActiveItems = new(
        ErrorType.Conflict, "Condition.HasActiveItems", "Condition is assigned to active items, cannot delete");
}
