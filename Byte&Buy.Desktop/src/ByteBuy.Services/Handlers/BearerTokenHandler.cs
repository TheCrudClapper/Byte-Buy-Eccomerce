using ByteBuy.Services.InfraContracts.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Services.Handlers;

/// <summary>
/// Attaches authentication token to http requests
/// </summary>
public class BearerTokenHandler(ITokenStore tokenStore) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(tokenStore.AccessToken))
            request.Headers.Add("Authorization", $"Bearer {tokenStore.AccessToken}");

        return base.SendAsync(request, cancellationToken);
    }
}

public static class BearerTokenHandlerExtensions
{
    public static IServiceCollection AddAuthHeaderHandler(this IServiceCollection services)
    {
        services.AddTransient<BearerTokenHandler>();
        return services;
    }
}