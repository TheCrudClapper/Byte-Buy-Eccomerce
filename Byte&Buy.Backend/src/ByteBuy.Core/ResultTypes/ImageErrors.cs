namespace ByteBuy.Core.ResultTypes;

public static class ImageErrors
{
    public static readonly Error WrongImageExtensions = 
        new(400, $"Images must have only .jpg, .jpeg, .png  extensions !");
}
