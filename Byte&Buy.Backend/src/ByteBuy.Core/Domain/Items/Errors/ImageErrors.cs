using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Items.Errors;

/// <summary>
/// Class describes errors that might occur while working with image entity
/// </summary>
public static class ImageErrors
{
    public static readonly Error WrongImageExtensions = new(
        ErrorType.Validation,
        "Image.WrongImageExtension",
        "Images must have only .jpg, .jpeg, .png  extensions !");

    public static readonly Error UnauthorizedAccess = new(
        ErrorType.Unauthorized,
        "Image.UnauthorizedAccess",
        "You are not allowed to modify these images.");

    public static readonly Error StorageFailure = new(
        ErrorType.Unexpected,
        "Image.StorageFailure",
        "Unable to remove images at this time.");

    public static readonly Error FolderDoesntExist = new(
       ErrorType.Unexpected,
       "Image.FolderDoesNotExist",
       "Could not save new images.");

    public static readonly Error AltTextInvalid = Error.Validation(
       "Image.AltText",
       "Alternative text must be at most 50 characters.");

    public static readonly Error ImagePathInvalid = Error.Validation(
       "Image.Path",
       "Image path is required.");
}
