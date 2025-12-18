namespace ByteBuy.Core.ResultTypes;

public static class ImageErrors
{
    public static readonly Error WrongImageExtensions =
        new(400, $"Images must have only .jpg, .jpeg, .png  extensions !");

    public static readonly Error UnauthorizedAccess =
        new(403, $"Unable to remove images at this time");

    public static readonly Error StorageFailure =
        new(500, $"Unable to remove image/s at this time");

    public static readonly Error FolderDoesntExist =
        new(500, $"Couldn't save new pictures");
}
