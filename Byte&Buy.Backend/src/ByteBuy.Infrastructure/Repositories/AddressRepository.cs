using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressRepository : BaseRepository, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == addressId, ct);
    }
}
