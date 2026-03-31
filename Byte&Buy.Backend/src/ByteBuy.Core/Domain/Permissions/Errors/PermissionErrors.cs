using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Permissions.Errors;

public static class PermissionErrors
{
    public static readonly Error InvalidName =
        Error.Validation("Permission.Name", "Name is required and must be at most 100 characters.");

    public static readonly Error InvalidDescription =
        Error.Validation("Permission.Description", "Description is required and must be at most 100 characters.");
}
