using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ByteBuy.Infrastructure.Repositories;

public class RentalRepository : EfBaseRepository<Rental>, IRentalRepository
{
    public RentalRepository(ApplicationDbContext context) : base(context){}

    public async Task<Rental?> GetUserRental(Guid userId, Guid rentalId, CancellationToken ct = default)
    {
        return await _context.Rentals
            .FirstOrDefaultAsync(r => r.Id == rentalId && r.BorrowerId == userId, ct);
    }
}