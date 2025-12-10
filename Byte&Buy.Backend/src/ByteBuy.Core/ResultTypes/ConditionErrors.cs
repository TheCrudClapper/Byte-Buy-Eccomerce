namespace ByteBuy.Core.ResultTypes;

public static class ConditionErrors
{
    public static readonly Error AlreadyExists = new Error
       (400, "Condition with this name already exists");

    public static readonly Error InUse = new Error
      (400, "Condition is used, cannot be deleted");
}
