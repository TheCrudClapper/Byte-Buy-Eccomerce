namespace ByteBuy.Services.Stores.Abstractions;

//runtime service that stores user auth token
public interface ITokenStore
{
    string? AccessToken { get; set; }
}