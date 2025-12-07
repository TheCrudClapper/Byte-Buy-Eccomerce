using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressRepository : EfBaseRepository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context) { }

    public async Task<bool> DoesAddressWithLabelExists(Guid userId, string label)
    {
        return await _context.PortalUsers
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Addresses)
            .AnyAsync(a => EF.Functions.ILike(a.Label, label));
    }
    public async Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == addressId, ct);
    }
}
