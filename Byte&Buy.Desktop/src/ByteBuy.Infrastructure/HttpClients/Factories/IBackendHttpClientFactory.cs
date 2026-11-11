namespace ByteBuy.Infrastructure.HttpClients.Factories;

public interface IBackendHttpClientFactory
{
    HttpClient CreateClient(bool requireAuth = true);
}
