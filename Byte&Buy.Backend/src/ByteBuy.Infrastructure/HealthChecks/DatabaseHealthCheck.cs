using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ByteBuy.Infrastructure.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ApplicationDbContext _context;
        public DatabaseHealthCheck(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try 
            {
                var canConnect = await _context.Database.CanConnectAsync();
                return canConnect 
                    ? HealthCheckResult.Healthy("Successfully connected to databse.") 
                    : HealthCheckResult.Unhealthy("Could not connect to database");
            }
            catch(Exception ex)
            {
                return HealthCheckResult.Unhealthy("An exception during healthcheck occured", ex);
            }
        }
    }
}
