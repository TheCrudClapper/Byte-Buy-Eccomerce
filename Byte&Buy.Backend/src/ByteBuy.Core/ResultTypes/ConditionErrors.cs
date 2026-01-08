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

    public static readonly Error NameInvalid = Error.Validation(
        "Condition.Name", "Name is required and must be at most 20 characters.");

    public static readonly Error DescriptionContentInvalid = Error.Validation(
        "Condition.Description", "Description cannot contain only whitespace.");

    public static readonly Error DescriptionLengthInvalid = Error.Validation(
        "Condition.Description", "Description must be at most 50 characters.");


}
