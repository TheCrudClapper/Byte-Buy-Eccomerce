namespace ByteBuy.API.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration config)
    { 
        var corsAudience = config.GetSection("Cors").GetValue<string>("AngularFrontend") ?? string.Empty;
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .WithOrigins(corsAudience)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
}
