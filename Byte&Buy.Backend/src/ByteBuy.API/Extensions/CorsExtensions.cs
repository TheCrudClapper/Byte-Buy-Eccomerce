namespace ByteBuy.API.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200", "http://192.168.1.5:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
}
