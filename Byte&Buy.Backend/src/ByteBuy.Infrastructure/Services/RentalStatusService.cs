using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Services;

public class RentalStatusService(ApplicationDbContext context) 
    : IRentalStatusService
{
    public async Task UpdateRentalStatusesAsync()
    {
        await context.Database.ExecuteSqlRawAsync(@"
            UPDATE ""Rentals""
            SET ""Status"" =
                CASE
                    WHEN (CURRENT_TIMESTAMP AT TIME ZONE 'UTC') > ""RentalEndDate"" THEN 2
                    WHEN (CURRENT_TIMESTAMP AT TIME ZONE 'UTC') >= ""RentalStartDate""
                         AND (CURRENT_TIMESTAMP AT TIME ZONE 'UTC') <= ""RentalEndDate""
                    THEN 1
                    ELSE ""Status""
                END
            WHERE ""Status"" IN (0, 1)
        ");
    }
}
