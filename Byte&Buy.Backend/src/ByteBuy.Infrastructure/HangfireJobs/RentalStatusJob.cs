using ByteBuy.Infrastructure.ServiceContracts;

namespace ByteBuy.Infrastructure.HangfireJobs;

public class RentalStatusJob
{
    private readonly IRentalStatusService _rentalStatusService;
    public RentalStatusJob(IRentalStatusService rentalStatusService)
       => _rentalStatusService = rentalStatusService;


    public async Task Execute()
    {
        await _rentalStatusService.UpdateRentalStatusesAsync();
    }
}
