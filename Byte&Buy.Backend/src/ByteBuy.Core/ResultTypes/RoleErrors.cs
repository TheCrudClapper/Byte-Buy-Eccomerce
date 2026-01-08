namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with system's roles
/// </summary>
public static class RoleErrors
{
    public static readonly Error NotFound = new(
       ErrorType.NotFound, "Role.NotFound", "Role is not found");

    public static readonly Error AlreadyExist = new(
        ErrorType.Conflict, "Role.RoleAlreadyExists", "Role of given name already exists");

    public static readonly Error HasActiveUsers = new(
        ErrorType.Conflict, "Role.HasActiveUsers", "Cannot delete a role with active users !");
}
