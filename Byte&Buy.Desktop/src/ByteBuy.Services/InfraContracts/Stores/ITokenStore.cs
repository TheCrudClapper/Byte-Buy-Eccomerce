namespace ByteBuy.Services.InfraContracts.Stores;

//runtime service that stores user auth token
public interface ITokenStore
{
    string? AccessToken { get; set; }
}