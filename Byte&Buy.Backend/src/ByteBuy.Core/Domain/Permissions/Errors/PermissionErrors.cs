using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Permissions.Errors;

public static class PermissionErrors
{
    public static readonly Error NotFound = new(
        ErrorType.NotFound, "Permission.NotFound", "Permission is not found");

    public static readonly Error InvalidName =
        Error.Validation("Permission.Name", "Name is required and must be at most 100 characters.");

    public static readonly Error InvalidDescription =
        Error.Validation("Permission.Description", "Description is required and must be at most 100 characters.");

    public static readonly Error AlreadyExists = new(
        ErrorType.Conflict, "Permission.AlreadyExists", "Permission with given name already exists");

    public static readonly Error HasActiveRelations = new(
        ErrorType.Conflict, "Permission.HasActiveRoles", "Permission is used, cannot be deleted");
}
