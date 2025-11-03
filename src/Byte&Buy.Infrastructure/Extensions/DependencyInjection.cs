using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Byte_Buy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
