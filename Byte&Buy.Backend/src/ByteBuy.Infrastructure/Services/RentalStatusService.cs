using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Infrastructure.ServiceContracts;

namespace ByteBuy.Infrastructure.Services;

public class RentalStatusService : IRentalStatusService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RentalStatusService(IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork)
    {
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;

    }

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

        await _unitOfWork.SaveChangesAsync();
    }
}
