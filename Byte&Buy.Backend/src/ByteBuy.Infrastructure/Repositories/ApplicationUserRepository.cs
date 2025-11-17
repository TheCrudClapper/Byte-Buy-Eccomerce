using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class ApplicationUserRepository : BaseRepository, IApplicationUserRepository
{
    public ApplicationUserRepository(ApplicationDbContext context) : base(context){}

    public Task<bool> ExistByEmailAsync(string email, CancellationToken ct = default)
    {
        return _context.Users
            .AsNoTracking()
            .AnyAsync(e => e.Email == email, ct);
    }
}
