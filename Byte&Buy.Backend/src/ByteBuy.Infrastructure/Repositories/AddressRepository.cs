using ByteBuy.Core.Domain.Entities;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class AddressRepository : BaseRepository, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context) { }

    public async Task AddAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task<Address?> GetByIdAsync(Guid addressId, CancellationToken ct = default)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == addressId, ct);
    }

    public async Task<Address?> GetUserAddress(Guid addressId, CancellationToken ct = default)
    {
        return await _context.Addresses
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == addressId)
    }

    public async Task<bool> DoesUserHaveAdresses(Guid userId)
    {
        return await _context.PortalUsers
            .AnyAsync(u => u.Id == userId & u.Addresses.Any());
    }

    public async Task<bool> DoesAddressWithLabelExists(Guid userId, string label)
    {
        return await _context.PortalUsers
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Addresses)
            .AnyAsync(a => EF.Functions.ILike(a.Label, label));
    }

    public async Task<IEnumerable<Address>> GetUserAdressesAsync(Guid userId, CancellationToken ct = default)
    {
        return await _context.PortalUsers
            .Where(u => u.Id == userId)
            .SelectMany(i => i.Addresses)
            .ToListAsync(ct);
    }

   
}
