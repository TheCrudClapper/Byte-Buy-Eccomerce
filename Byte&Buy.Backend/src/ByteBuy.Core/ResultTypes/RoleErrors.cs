namespace ByteBuy.Core.ResultTypes;

/// <summary>
/// Class describes errors that might occur while working with system's roles
/// </summary>
public static class RoleErrors
{
    public static readonly Error NotFound = new(
       ErrorType.NotFound, "Role.NotFound", "Role is not found");

    public static readonly Error SystemRoleDeleteForbidden = new(
       ErrorType.NotFound, "Role.SystemRoleDeleteForbidden", "System role deletion is forbidden");

    public static readonly Error AlreadyExist = new(
        ErrorType.Conflict, "Role.RoleAlreadyExists", "Role of given name already exists");

    public static readonly Error HasActiveUsers = new(
        ErrorType.Conflict, "Role.HasActiveUsers", "Cannot delete a role with active users !");

    public static readonly Error InvalidName =
        Error.Validation("Role.Name", "Name is required and must be at most 20 characters.");

    public static readonly Error PermissionIsRequired =
        Error.Validation("Role.Permission", "Role need to have at least one permission");
}
