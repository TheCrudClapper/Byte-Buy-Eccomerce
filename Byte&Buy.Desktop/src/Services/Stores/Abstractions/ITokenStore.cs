namespace ByteBuy.Services.Stores.Abstractions;

public interface ITokenStore
{
    string? AccessToken { get; set; }
}