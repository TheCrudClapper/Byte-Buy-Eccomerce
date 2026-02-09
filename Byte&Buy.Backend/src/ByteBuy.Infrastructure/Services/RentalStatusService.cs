using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.ServiceContracts;

namespace ByteBuy.Infrastructure.Services;

public class RentalStatusService : IRentalStatusService
{
    private readonly IRentalRepository _rentalRepository;
    public RentalStatusService(IRentalRepository rentalRepository)
      => _rentalRepository = rentalRepository;


    public async Task UpdateRentalStatusesAsync()
    {
        var statuses = await _rentalRepository
            .GetAllByConditionAsync(r => r.Status == RentalStatus.Created || r.Status == RentalStatus.Active);

        var now = DateTime.UtcNow;

        foreach (var rental in statuses)
        {
            if (now < rental.RentalStartDate)
                continue;

            if (now > rental.RentalEndDate)
            {
                if (rental.Status != RentalStatus.Overdue)
                    rental.MarkAsOverdue();

                continue;
            }

            if (rental.Status != RentalStatus.Active)
                rental.ActivateRental();
        }

        await _rentalRepository.CommitAsync();

    }
}
