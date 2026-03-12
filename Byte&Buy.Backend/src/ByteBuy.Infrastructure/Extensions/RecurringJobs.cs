using ByteBuy.Infrastructure.HangfireJobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace ByteBuy.Infrastructure.Extensions;

public static class RecurringJobs
{
    public static void AddRecurringJobs(this WebApplication app)
    {
        RecurringJob.AddOrUpdate<RentalStatusJob>(
            "update-rental-statuses",
            job => job.Execute(),
            Cron.Daily(0, 0)
        );

        RecurringJob.AddOrUpdate<OrderStatusJob>(
            "cancel-unpaid-orders",
            job => job.Execute(),
            Cron.Daily(0, 0)
        );
    }
}
