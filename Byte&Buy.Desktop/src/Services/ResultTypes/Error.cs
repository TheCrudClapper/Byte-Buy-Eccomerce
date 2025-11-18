namespace ByteBuy.Services.ResultTypes;

public sealed record Error(string Description)
{
    public static readonly Error ConectivityError = new Error("Connection failed, try again later");
};