namespace ByteBuy.Services.ResultTypes;

public static class ApiErrors
{
    public static readonly Error FetchedResourceIsNull = new Error("Invalid response from API");
    public static readonly Error UnknownError = new Error("Unknown error occured");
}